﻿
using System;
using System.Collections.Generic;

namespace DScript.Compiler
{
    /// <summary>
    /// 语法树
    /// </summary>
    public class SyntaxTree
    {
        /// <summary>
        /// 根命名空间
        /// </summary>
        public NamespaceNode Root { get; } = new NamespaceNode("#global", "global", null);

        private SyntaxTreeParse _treeParse;
        //已经解析过的文件的信息
        private Dictionary<string, FileToken> _fileTokens = new Dictionary<string, FileToken>();
        private string _currFile;
        private FileToken _currFileToken;
        private int _currIndex = -1;

        public SyntaxTree()
        {
            _treeParse = new SyntaxTreeParse(this);
        }

        public void ParseToken(string path, Token[] tokens)
        {
            if (_fileTokens.ContainsKey(path))
            {
                //相同的文件名
                throw new System.Exception("xxx");
            }

            FileToken fileToken = new FileToken(path, tokens, this);
            _currFile = path;
            _fileTokens.Add(path, fileToken);
            _currFileToken = fileToken;

            _currIndex = -1;
            Token? current;
            //遍历所有token, 解析语法
            while ((current = GetNextToken()) != null)
            {
                switch (current.Type)
                {
                    case TokenType.Keyword: //关键字
                        _treeParse.NextKeyword(current, fileToken);
                        break;
                    case TokenType.Word: //单词
                        _treeParse.NextWorld(current, fileToken);
                        break;
                    default:
                        LogUtils.Error("未知字符: " + current.Code);
                        break;
                }
            }
        }

        /// <summary>
        /// 获取当前正在解析的token
        /// </summary>
        internal Token? GetCurrentToken()
        {
            if (_currFile == null || !_fileTokens.ContainsKey(_currFile))
            {
                return null;
            }

            var tokens = _fileTokens[_currFile].Tokens;
            if (_currIndex < tokens.Length)
            {
                return tokens[_currIndex];
            }

            return null;
        }

        /// <summary>
        /// 获取指定索引的token
        /// </summary>
        internal Token? GetToken(int index)
        {
            if (_currFile == null || !_fileTokens.ContainsKey(_currFile))
            {
                return null;
            }

            var tokens = _fileTokens[_currFile].Tokens;
            if (index < tokens.Length)
            {
                return tokens[index];
            }

            return null;
        }
        
        /// <summary>
        /// 获取下一个token, 会移动指针
        /// </summary>
        internal Token? GetNextToken()
        {
            if (_currFileToken == null)
            {
                return null;
            }

            _currIndex++;
            var tokens = _currFileToken.Tokens;
            if (_currIndex < tokens.Length)
            {
                return tokens[_currIndex];
            }

            return null;
        }

        /// <summary>
        /// 获取下一个非换行 token, 会移动指针, 返回的 bool 代表是否碰到换行
        /// </summary>
        internal bool GetNextTokenIgnoreLineFeed(out Token token)
        {
            if (_currFileToken == null)
            {
                token = null;
                return false;
            }

            _currIndex++;
            var tokens = _currFileToken.Tokens;

            bool flag = false;
            for (; _currIndex < tokens.Length; _currIndex++)
            {
                var tk = tokens[_currIndex];
                if (tk.Type == TokenType.LineFeed) //匹配到换行
                {
                    flag = true;
                }
                else
                {
                    token = tk;
                    return flag;
                }
            }

            token = null;
            return flag;
        }

        /// <summary>
        /// 尝试获取下一个token, 不会移动指针
        /// </summary>
        /// <param name="offset">获取token的偏移量</param>
        internal Token? TryGetNextToken(int offset = 1)
        {
            if (_currFileToken == null)
            {
                return null;
            }

            var tokens = _currFileToken.Tokens;
            if (_currIndex < tokens.Length - offset)
            {
                return tokens[_currIndex + offset];
            }

            return null;
        }

        /// <summary>
        /// 尝试获取下一个 token, 忽略换行, 不会移动指针
        /// </summary>
        /// <param name="offset">获取token的偏移量</param>
        internal bool TryGetNextTokenIgnoreLineFeed(out Token token, int offset = 1)
        {
            if (_currFileToken == null)
            {
                token = null;
                return false;
            }

            var tokens = _currFileToken.Tokens;

            bool flag = false;
            for (var i = _currIndex + 1; i < tokens.Length; i++)
            {
                var tk = tokens[i];
                if (tk.Type == TokenType.LineFeed) //匹配到换行
                {
                    flag = true;
                }
                else
                {
                    token = tk;
                    return flag;
                }
            }

            token = null;
            return flag;
        }

        /// <summary>
        /// 返回是否有下一个token
        /// </summary>
        internal bool HasNextToken()
        {
            if (_currFileToken == null)
            {
                return false;
            }

            var tokens = _currFileToken.Tokens;
            return _currIndex < tokens.Length - 1;
        }

        /// <summary>
        /// 回退解析索引, 也就是 index-1
        /// </summary>
        internal int RollbackTokenIndex()
        {
            if (_currIndex == -1)
            {
                return -1;
            }

            if (_currIndex > 0)
            {
                _currIndex--;
            }
            else
            {
                _currIndex = 0;
            }

            return _currIndex;
        }

        /// <summary>
        /// 回退到指定的解析索引
        /// </summary>
        internal void RollbackTokenIndex(int index)
        {
            _currIndex = index;
        }
        
        /// <summary>
        /// 获取解析索引
        /// </summary>
        internal int GetTokenIndex()
        {
            return _currIndex;
        }
        
        /// <summary>
        /// 返回token长度
        /// </summary>
        internal int GetTokenLength()
        {
            if (_currFileToken == null)
            {
                return 0;
            }

            return _currFileToken.Tokens.Length;
        }

        /// <summary>
        /// 复制指定位置 Token 并返回
        /// </summary>
        /// <param name="start">起始位置</param>
        /// <param name="end">结束位置</param>
        /// <param name="ignoreLineFeed">是否忽略换行</param>
        internal Token[] CopyTokens(int start, int end, bool ignoreLineFeed = true)
        {
            if (_currFileToken == null)
            {
                return null;
            }

            var tokens = _currFileToken.Tokens;
            List<Token> newArr = new List<Token>();
            for (int i = start; i < end; i++)
            {
                var temp = tokens[i];
                if (!ignoreLineFeed || temp.Type != TokenType.LineFeed)
                {
                    newArr.Add(temp);
                }
            }

            return newArr.ToArray();
        }
    }
}