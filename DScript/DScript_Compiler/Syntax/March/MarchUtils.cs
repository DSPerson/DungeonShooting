﻿using System;

namespace DScript.Compiler
{
    /// <summary>
    /// 匹配词法工具类
    /// </summary>
    internal static class MarchUtils
    {

        public static void March(SyntaxTree syntaxTree, MarchData[] marchTypes, Action<MarchResult> callback)
        {
            //记录起始位置
            var marchResult = new MarchResult();
            marchResult.Start = syntaxTree.GetTokenIndex() + 1;
            for (var i = 0; i < marchTypes.Length; i++)
            {
                var item = marchTypes[i];

                if (item.MarchType == MarchData.MarchDataType.Code) //匹配字符串
                {
                    syntaxTree.GetNextTokenIgnoreLineFeed(out var tempToken);
                    if (tempToken.Code != item.Code) //匹配失败
                    {
                        marchResult.Success = false;
                        marchResult.End = syntaxTree.GetTokenIndex();
                        callback(marchResult);
                        return;
                    }
                }
                else if (item.MarchType == MarchData.MarchDataType.MarchType) //根据枚举类型匹配
                {
                    if (item.Type == MarchType.Word) //匹配单个单词
                    {
                        syntaxTree.GetNextTokenIgnoreLineFeed(out var tempToken);
                        if (tempToken.Type != TokenType.Word) //匹配失败
                        {
                            marchResult.Success = false;
                            marchResult.End = syntaxTree.GetTokenIndex();
                            callback(marchResult);
                            return;
                        }
                    }
                    else if (item.Type == MarchType.FullWord || item.Type == MarchType.FullKeyword) //匹配全路径
                    {
                        if (!MarchFullName(syntaxTree, item.Type == MarchType.FullKeyword)) //匹配失败
                        {
                            marchResult.Success = false;
                            marchResult.End = syntaxTree.GetTokenIndex();
                            callback(marchResult);
                            return;
                        }
                    }
                    else if (item.Type == MarchType.ParenthesesGroup) //匹配小括号
                    {
                        if (!MarchGroup(syntaxTree, TokenType.ParenthesesLeft, TokenType.ParenthesesRight)) //匹配失败
                        {
                            marchResult.Success = false;
                            marchResult.End = syntaxTree.GetTokenIndex();
                            callback(marchResult);
                            return;
                        }
                    }
                    else if (item.Type == MarchType.BracketGroup) //匹配中括号
                    {
                        if (!MarchGroup(syntaxTree, TokenType.BracketLeft, TokenType.BracketRight)) //匹配失败
                        {
                            marchResult.Success = false;
                            marchResult.End = syntaxTree.GetTokenIndex();
                            callback(marchResult);
                            return;
                        }
                    }
                    else if (item.Type == MarchType.BraceGroup) //匹配大括号
                    {
                        if (!MarchGroup(syntaxTree, TokenType.BraceLeft, TokenType.BraceRight)) //匹配失败
                        {
                            marchResult.Success = false;
                            marchResult.End = syntaxTree.GetTokenIndex();
                            callback(marchResult);
                            return;
                        }
                    }
                    else if (item.Type == MarchType.Expression) //表达式
                    {
                        if (!MarchExpression(syntaxTree))
                        {
                            marchResult.Success = false;
                            marchResult.End = syntaxTree.GetTokenIndex();
                            callback(marchResult);
                            return;
                        }
                    }
                }
                else //尝试匹配
                {
                    var index = syntaxTree.GetTokenIndex();
                    MarchResult tempResult = null;
                    March(syntaxTree, item.TryMarch, result => { tempResult = result; });
                    if (tempResult != null && !tempResult.Success) //匹配失败
                    {
                        if (IsEmptyOrLineFeed(syntaxTree, tempResult)) //回退索引
                        {
                            syntaxTree.RollbackTokenIndex(index);
                        }
                        else //匹配失败
                        {
                            marchResult.Success = false;
                            marchResult.End = tempResult.End;
                            callback(marchResult);
                            return;
                        }
                    }
                }
            }

            marchResult.Success = true;
            marchResult.End = (int)MathF.Min(syntaxTree.GetTokenIndex() + 1, syntaxTree.GetTokenLength());
            callback(marchResult);
        }

        private static bool IsEmptyOrLineFeed(SyntaxTree syntaxTree, MarchResult result)
        {
            if (result.Start == result.End)
            {
                return true;
            }

            for (int i = result.Start; i < result.End; i++)
            {
                var temp = syntaxTree.GetToken(i);
                if (temp == null)
                {
                    return true;
                }

                if (temp.Type != TokenType.LineFeed)
                {
                    return false;
                }
            }

            return true;
        }

        //匹配全路径名称, 返回是否匹配成功
        private static bool MarchFullName(SyntaxTree syntaxTree, bool isFullKeyword)
        {
            //是否能结束匹配
            bool canEnd = false;
            Token tempToken;

            while (syntaxTree.HasNextToken())
            {
                //获取下一个token
                var lineFeed = syntaxTree.GetNextTokenIgnoreLineFeed(out tempToken);
                if (canEnd)
                {
                    if (tempToken == null) //文件结束
                    {
                        //完成
                        return true;
                    }
                    else if (tempToken.Type == TokenType.Dot) //碰到逗号, 继续匹配
                    {
                        canEnd = false;
                    }
                    else if (tempToken.Type == TokenType.Semicolon) //碰到分号, 直接结束
                    {
                        //完成
                        return true;
                    }
                    else if (lineFeed) //换了行, 结束
                    {
                        //回滚索引
                        syntaxTree.RollbackTokenIndex();
                        //完成
                        return true;
                    }
                    else if (tempToken.Type != TokenType.Keyword && tempToken.Type != TokenType.Word)
                    {
                        //回滚索引
                        syntaxTree.RollbackTokenIndex();
                        return true;
                    }
                    else //语法错误
                    {
                        return false;
                    }
                }
                else
                {
                    if (CheckType(tempToken, isFullKeyword))
                    {
                        canEnd = true;
                    }
                    else //语法错误
                    {
                        return false;
                    }
                }
            }

            return canEnd;
        }

        //匹配组, 返回是否匹配成功
        private static bool MarchGroup(SyntaxTree syntaxTree, TokenType left, TokenType right)
        {
            syntaxTree.GetNextTokenIgnoreLineFeed(out var tempToken);
            if (tempToken != null && tempToken.Type == left)
            {
                int count = 1;
                while (syntaxTree.HasNextToken())
                {
                    syntaxTree.GetNextTokenIgnoreLineFeed(out tempToken);
                    if (tempToken.Type == left)
                    {
                        count++;
                    }
                    else if (tempToken.Type == right)
                    {
                        count--;
                        if (count == 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        //匹配表达式
        private static bool MarchExpression(SyntaxTree syntaxTree)
        {
            syntaxTree.GetNextTokenIgnoreLineFeed(out var token);
            if (token.Type == TokenType.Word) //单词
            {
                syntaxTree.GetNextTokenIgnoreLineFeed(out var tempToken);
                if (tempToken == null || tempToken.Type == TokenType.Word) //第一种情况: 单个单词, 例如: 1, "a", 1.5, false
                {
                    return true;
                }
            }
            
            return true;
        }
        
        private static bool CheckType(Token token, bool isFullKeyword)
        {
            if (isFullKeyword)
            {
                return token.Type == TokenType.Keyword || token.Type == TokenType.Word;
            }

            return token.Type == TokenType.Word;
        }
    }
}