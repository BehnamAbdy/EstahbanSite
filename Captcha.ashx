<%@ WebHandler Language="C#" Class="Captcha" %>

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public class Captcha : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["mode"] != null)
        {
            string code = RandomImage.GenerateRandomCode();
            RandomImage randImg = new RandomImage(code, 123, 30);            

            switch (context.Request.QueryString["mode"])
            {
                case "1": // Balance.aspx
                    context.Session["Captcha1"] = code;
                    break;
                    
                case "2": // Transactions.aspx
                    context.Session["Captcha2"] = code;
                    break;

                case "3": // TransCash.aspx
                    context.Session["Captcha3"] = code;
                    break;

                case "4": // Loan.aspx
                    context.Session["Captcha4"] = code;
                    break;

                case "5": // LoanRecords.aspx
                    context.Session["Captcha5"] = code;
                    break;

                case "6": // EditPass.aspx
                    context.Session["Captcha6"] = code;
                    break;

                case "7": // BMI/Loan.aspx
                    context.Session["Captcha7"] = code;
                    break;

                case "8": // BMI/TransCash.aspx
                    context.Session["Captcha8"] = code;
                    break;
            }

            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";
            randImg.Image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            randImg.Dispose();
        }
        else if (context.Request.QueryString["cls"] != null)
        {
            switch (context.Request.QueryString["mode"])
            {
                case "1": // Balance.aspx
                    context.Session["Captcha1"] = null;
                    break;

                case "2": // Transactions.aspx
                    context.Session["Captcha2"] = null;
                    break;

                case "3": // TransCash.aspx
                    context.Session["Captcha3"] = null;
                    break;

                case "4": // Loan.aspx
                    context.Session["Captcha4"] = null;
                    break;

                case "5": // LoanRecords.aspx
                    context.Session["Captcha5"] = null;
                    break;

                case "6": // EditPass.aspx
                    context.Session["Captcha6"] = null;
                    break;

                case "7": // BMI/Loan.aspx
                    context.Session["Captcha7"] = null;
                    break;

                case "8": // BMI/TransCash.aspx
                    context.Session["Captcha8"] = null;
                    break;
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

class RandomImage
{
    private string text;
    private int width;
    private int height;
    private Bitmap image;
    private Random random = new Random();

    public string Text
    {
        get { return this.text; }
    }

    public Bitmap Image
    {
        get { return this.image; }
    }

    public int Width
    {
        get { return this.width; }
    }

    public int Height
    {
        get { return this.height; }
    }

    public RandomImage(string text, int width, int height)
    {
        this.text = text;
        this.SetDimensions(width, height);
        this.GenerateImage();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            this.image.Dispose();
    }

    private void SetDimensions(int width, int height)
    {
        if (width <= 0)
            throw new ArgumentOutOfRangeException("width", width,
                "Argument out of range, must be greater than zero.");
        if (height <= 0)
            throw new ArgumentOutOfRangeException("height", height,
                "Argument out of range, must be greater than zero.");
        this.width = width;
        this.height = height;
    }

    private void GenerateImage()
    {
        Bitmap bitmap = new Bitmap
          (this.width, this.height, PixelFormat.Format32bppArgb);
        Graphics g = Graphics.FromImage(bitmap);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle rect = new Rectangle(0, 0, this.width, this.height);
        System.Drawing.Drawing2D.HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti,
            Color.LightGray, Color.White);
        g.FillRectangle(hatchBrush, rect);
        SizeF size;
        float fontSize = rect.Height + 1;
        Font font;

        do
        {
            fontSize--;
            font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
            size = g.MeasureString(this.text, font);
        } while (size.Width > rect.Width);
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        GraphicsPath path = new GraphicsPath();
        //path.AddString(this.text, font.FontFamily, (int) font.Style, 
        //    font.Size, rect, format);
        path.AddString(this.text, font.FontFamily, (int)font.Style, 30, rect, format);
        float v = 4F;
        PointF[] points =
          {
                new PointF(this.random.Next(rect.Width) / v, this.random.Next(
                   rect.Height) / v),
                new PointF(rect.Width - this.random.Next(rect.Width) / v, 
                    this.random.Next(rect.Height) / v),
                new PointF(this.random.Next(rect.Width) / v, 
                    rect.Height - this.random.Next(rect.Height) / v),
                new PointF(rect.Width - this.random.Next(rect.Width) / v,
                    rect.Height - this.random.Next(rect.Height) / v)
          };
        Matrix matrix = new Matrix();
        matrix.Translate(0F, 0F);
        path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
        hatchBrush = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.SkyBlue);
        g.FillPath(hatchBrush, path);
        int m = Math.Max(rect.Width, rect.Height);
        for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
        {
            int x = this.random.Next(rect.Width);
            int y = this.random.Next(rect.Height);
            int w = this.random.Next(m / 50);
            int h = this.random.Next(m / 50);
            g.FillEllipse(hatchBrush, x, y, w, h);
        }
        font.Dispose();
        hatchBrush.Dispose();
        g.Dispose();
        this.image = bitmap;
    }

    public static string GenerateRandomCode()
    {
        Random r = new Random();
        return r.Next(10001, 99999).ToString();
        string code = "";
        for (int j = 0; j < 4; j++)
        {
            int i = r.Next(3);
            int ch;
            switch (i)
            {
                case 1:
                    ch = r.Next(0, 9);
                    code = code + ch.ToString();
                    break;
                case 2:
                    ch = r.Next(65, 90);
                    code = code + Convert.ToChar(ch).ToString();
                    break;
                case 3:
                    ch = r.Next(97, 122);
                    code = code + Convert.ToChar(ch).ToString();
                    break;
                default:
                    ch = r.Next(97, 122);
                    code = code + Convert.ToChar(ch).ToString();
                    break;
            }
            r.NextDouble();
            r.Next(100, 1999);
        }
        return code;
    }
}