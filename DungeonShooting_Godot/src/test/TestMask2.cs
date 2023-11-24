using Godot;
using System;
using System.Collections.Generic;

public partial class TestMask2 : SubViewportContainer
{
    public class ImageData
    {
        public int Width;
        public int Height;
        public PixelData[] Pixels;
        
        public ImageData(Image image)
        {
            var list = new List<PixelData>();
            var width = image.GetWidth();
            var height = image.GetHeight();
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    if (pixel.A > 0)
                    {
                        list.Add(new PixelData()
                        {
                            X = x,
                            Y = y,
                            Color = pixel
                        });
                    }
                }
            }

            Pixels = list.ToArray();
            Width = width;
            Height = height;
        }
    }
    
    public class PixelData
    {
        public int X;
        public int Y;
        public Color Color;
    }

    public class ImagePixel
    {
        public int X;
        public int Y;
        public Color Color;
        public byte Type;
        public float Timer;
        public float Speed;
        public bool IsRun;
        public float TempTime;
    }
    
    [Export]
    public Sprite2D Canvas;

    [Export]
    public Texture2D Brush1;
    [Export]
    public Texture2D Brush2;

    private ImageData _brushData1;
    private ImageData _brushData2;
    private Image _image;
    private ImageTexture _texture;

    private ImagePixel[,] _imagePixels;
    private List<ImagePixel> _cacheImagePixels = new List<ImagePixel>();
    private float _runTime = 0;
    private int _executeIndex = -1;

    //程序每帧最多等待执行时间, 超过这个时间的像素点将交到下一帧执行, 单位: 毫秒
    private float _maxWaitTime = 1f;
    
    public override void _Ready()
    {
        _brushData1 = new ImageData(Brush1.GetImage());
        _brushData2 = new ImageData(Brush2.GetImage());
        _image = Image.Create(480, 270, false, Image.Format.Rgba8);
        _texture = ImageTexture.CreateFromImage(_image);
        Canvas.Texture = _texture;
        _imagePixels = new ImagePixel[480, 270];
    }

    public override void _Process(double delta)
    {
        var time = DateTime.UtcNow;
        //更新消除逻辑
        if (_cacheImagePixels.Count > 0)
        {
            var startIndex = _executeIndex;
            if (_executeIndex < 0 || _executeIndex >= _cacheImagePixels.Count)
            {
                _executeIndex = _cacheImagePixels.Count - 1;
            }

            var startTime = DateTime.UtcNow;
            var isOver = false;
            var index = 0;
            for (; _executeIndex >= 0; _executeIndex--)
            {
                index++;
                var imagePixel = _cacheImagePixels[_executeIndex];
                if (UpdateImagePixel(imagePixel)) //移除
                {
                    _cacheImagePixels.RemoveAt(_executeIndex);
                    if (_executeIndex < startIndex)
                    {
                        startIndex--;
                    }
                }

                if (index > 200)
                {
                    index = 0;
                    if ((DateTime.UtcNow - startTime).TotalMilliseconds > _maxWaitTime) //超过最大执行时间
                    {
                        isOver = true;
                        break;
                    }
                }
            }

            if (!isOver && startIndex >= 0 && _executeIndex < 0)
            {
                _executeIndex = _cacheImagePixels.Count - 1;
                for (; _executeIndex >= startIndex; _executeIndex--)
                {
                    index++;
                    var imagePixel = _cacheImagePixels[_executeIndex];
                    if (UpdateImagePixel(imagePixel)) //移除
                    {
                        _cacheImagePixels.RemoveAt(_executeIndex);
                    }
                    
                    if (index > 200)
                    {
                        index = 0;
                        if ((DateTime.UtcNow - startTime).TotalMilliseconds > _maxWaitTime) //超过最大执行时间
                        {
                            break;
                        }
                    }
                }
            }
        }
       
        var pos = (GetGlobalMousePosition() / 4).AsVector2I();
        if (Input.IsMouseButtonPressed(MouseButton.Left)) //绘制画笔1
        {
            DrawBrush(_brushData1, pos, 5f, 3);
        }
        else  if (Input.IsMouseButtonPressed(MouseButton.Right))  //绘制画笔2
        {
            DrawBrush(_brushData2, pos, 5f, 3);
        }

        //碰撞检测
        if (Input.IsKeyPressed(Key.Space))
        {
            var mousePosition = GetGlobalMousePosition();
            var pixel = _image.GetPixel((int)mousePosition.X / 4, (int)mousePosition.Y / 4);
            Debug.Log("是否碰撞: " + (pixel.A > 0));
        }
        
        _texture.Update(_image);
        _runTime += (float)delta;
        Debug.Log("用时: " + (DateTime.UtcNow - time).TotalMilliseconds);
    }

    private bool UpdateImagePixel(ImagePixel imagePixel)
    {
        if (imagePixel.Color.A > 0)
        {
            if (imagePixel.Timer > 0)
            {
                imagePixel.Timer -= _runTime - imagePixel.TempTime;
                imagePixel.TempTime = _runTime;
            }
            else
            {
                imagePixel.Color.A -= imagePixel.Speed * (_runTime - imagePixel.TempTime);
                _image.SetPixel(imagePixel.X, imagePixel.Y, imagePixel.Color);
                if (imagePixel.Color.A <= 0)
                {
                    imagePixel.IsRun = false;
                    return true;
                }
                else
                {
                    imagePixel.TempTime = _runTime;
                }
            }
        }

        return false;
    }

    private void DrawBrush(ImageData brush, Vector2I position, float duration, float writeOffTime)
    {
        var pos = position - new Vector2I(brush.Width, brush.Height) / 2;
        var canvasWidth = _texture.GetWidth();
        var canvasHeight = _texture.GetHeight();
        foreach (var brushPixel in brush.Pixels)
        {
            var x = pos.X + brushPixel.X;
            var y = pos.Y + brushPixel.Y;
            if (x >= 0 && x < canvasWidth && y >= 0 && y < canvasHeight)
            {
                _image.SetPixel(x, y, brushPixel.Color);
                var temp = _imagePixels[x, y];
                if (temp == null)
                {
                    temp = new ImagePixel()
                    {
                        X = x,
                        Y = y,
                        Color = brushPixel.Color,
                        Type = 0,
                        Timer = duration,
                        Speed = brushPixel.Color.A / writeOffTime,
                    };
                    _imagePixels[x, y] = temp;
                    
                    _cacheImagePixels.Add(temp);
                    temp.IsRun = true;
                    temp.TempTime = _runTime;
                }
                else
                {
                    temp.Color = brushPixel.Color;
                    temp.Speed = brushPixel.Color.A / writeOffTime;
                    temp.Timer = duration;
                    if (!temp.IsRun)
                    {
                        _cacheImagePixels.Add(temp);
                        temp.IsRun = true;
                        temp.TempTime = _runTime;
                    }
                }
            }
        }
    }
}
