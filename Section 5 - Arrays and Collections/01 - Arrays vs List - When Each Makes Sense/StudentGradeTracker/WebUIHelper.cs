using System.Diagnostics;
using System.Net;
using System.Text;

public static class WebUIHelper
{
    private static HttpListener? listener;
    private static string currentHtml = "";
    private static readonly int port = 8080;

    public static void StartServer()
    {
        if (listener != null) return;

        listener = new HttpListener();
        listener.Prefixes.Add($"http://localhost:{port}/");
        listener.Start();

        Console.WriteLine($"🌐 Web UI started at: http://localhost:{port}");
        Console.WriteLine("📱 Opening browser...\n");

        // Open browser
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"http://localhost:{port}",
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not open browser: {ex.Message}");
        }

        // Handle requests in background
        Task.Run(HandleRequests);
    }

    private static async void HandleRequests()
    {
        while (listener != null && listener.IsListening)
        {
            try
            {
                var context = await listener.GetContextAsync();
                var response = context.Response;

                byte[] buffer = Encoding.UTF8.GetBytes(currentHtml);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html; charset=utf-8";

                await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
            catch (Exception)
            {
                // Ignore errors when shutting down
                break;
            }
        }
    }

    public static void ShowArrayVsList(string[] array, List<string> list,
        int arraySize, int listInitialCount, int listAfterAdding, int listFinalCount)
    {
        StartServer();

        var html = GenerateArrayVsListHTML(array, list, arraySize, listInitialCount, listAfterAdding, listFinalCount);
        currentHtml = html;

        Console.WriteLine("✨ Data displayed in browser!");
        Console.WriteLine("Press any key to continue...");
    }

    private static string GenerateArrayVsListHTML(string[] array, List<string> list,
        int arraySize, int listInitialCount, int listAfterAdding, int listFinalCount)
    {
        return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Student Grade Tracker - Lesson 1</title>
    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}
        
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #1E1E1E 0%, #2D2D30 100%);
            color: #FFFFFF;
            min-height: 100vh;
            padding: 20px;
        }}
        
        .container {{
            max-width: 1200px;
            margin: 0 auto;
        }}
        
        .header {{
            text-align: center;
            margin-bottom: 40px;
            animation: fadeInDown 1s ease-out;
        }}
        
        .header h1 {{
            color: #512BD4;
            font-size: 2.5rem;
            margin-bottom: 10px;
            text-shadow: 0 0 20px rgba(81, 43, 212, 0.3);
        }}
        
        .header p {{
            color: #CCCCCC;
            font-size: 1.2rem;
        }}
        
        .comparison-grid {{
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 30px;
            margin-bottom: 40px;
        }}
        
        .card {{
            background: rgba(45, 45, 48, 0.8);
            border-radius: 15px;
            padding: 30px;
            border: 1px solid rgba(81, 43, 212, 0.2);
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.3);
            transition: all 0.3s ease;
            animation: slideInUp 0.8s ease-out;
        }}
        
        .card:hover {{
            transform: translateY(-5px);
            box-shadow: 0 20px 40px rgba(81, 43, 212, 0.2);
            border-color: rgba(81, 43, 212, 0.5);
        }}
        
        .card-title {{
            color: #E91E63;
            font-size: 1.8rem;
            margin-bottom: 20px;
            text-align: center;
            font-weight: 600;
        }}
        
        .array-card .card-title {{
            color: #F7931E;
        }}
        
        .list-card .card-title {{
            color: #16C60C;
        }}
        
        .items-container {{
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            margin-bottom: 20px;
        }}
        
        .item {{
            background: linear-gradient(45deg, #512BD4, #E91E63);
            color: white;
            padding: 8px 16px;
            border-radius: 20px;
            font-size: 0.9rem;
            font-weight: 500;
            animation: popIn 0.5s ease-out;
            animation-fill-mode: both;
        }}
        
        .array-item {{
            background: linear-gradient(45deg, #F7931E, #FF6B35);
        }}
        
        .list-item {{
            background: linear-gradient(45deg, #16C60C, #4CAF50);
        }}
        
        .stats {{
            background: rgba(255, 255, 255, 0.05);
            border-radius: 10px;
            padding: 15px;
            margin-top: 20px;
        }}
        
        .stat-row {{
            display: flex;
            justify-content: space-between;
            margin-bottom: 8px;
            font-size: 0.95rem;
        }}
        
        .stat-label {{
            color: #CCCCCC;
        }}
        
        .stat-value {{
            color: #FFFFFF;
            font-weight: 600;
        }}
        
        .progress-section {{
            background: rgba(45, 45, 48, 0.8);
            border-radius: 15px;
            padding: 30px;
            border: 1px solid rgba(81, 43, 212, 0.2);
            animation: slideInUp 1s ease-out;
        }}
        
        .progress-title {{
            color: #512BD4;
            font-size: 1.5rem;
            margin-bottom: 20px;
            text-align: center;
        }}
        
        .progress-steps {{
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
        }}
        
        .step {{
            display: flex;
            flex-direction: column;
            align-items: center;
            flex: 1;
        }}
        
        .step-number {{
            width: 40px;
            height: 40px;
            border-radius: 50%;
            background: linear-gradient(45deg, #512BD4, #E91E63);
            display: flex;
            align-items: center;
            justify-content: center;
            color: white;
            font-weight: bold;
            margin-bottom: 10px;
            animation: pulse 2s infinite;
        }}
        
        .step-label {{
            color: #CCCCCC;
            font-size: 0.9rem;
            text-align: center;
        }}
        
        .step-value {{
            color: #16C60C;
            font-weight: bold;
            font-size: 1.1rem;
        }}
        
        .arrow {{
            color: #512BD4;
            font-size: 1.5rem;
            margin: 0 10px;
        }}
        
        @keyframes fadeInDown {{
            from {{
                opacity: 0;
                transform: translateY(-30px);
            }}
            to {{
                opacity: 1;
                transform: translateY(0);
            }}
        }}
        
        @keyframes slideInUp {{
            from {{
                opacity: 0;
                transform: translateY(50px);
            }}
            to {{
                opacity: 1;
                transform: translateY(0);
            }}
        }}
        
        @keyframes popIn {{
            from {{
                opacity: 0;
                transform: scale(0.8);
            }}
            to {{
                opacity: 1;
                transform: scale(1);
            }}
        }}
        
        @keyframes pulse {{
            0%, 100% {{
                transform: scale(1);
            }}
            50% {{
                transform: scale(1.1);
            }}
        }}
        
        .highlight {{
            background: rgba(81, 43, 212, 0.1);
            border-left: 4px solid #512BD4;
            padding: 15px;
            border-radius: 5px;
            margin: 20px 0;
        }}
        
        @media (max-width: 768px) {{
            .comparison-grid {{
                grid-template-columns: 1fr;
            }}
            
            .progress-steps {{
                flex-direction: column;
                gap: 20px;
            }}
            
            .arrow {{
                transform: rotate(90deg);
            }}
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🎓 Student Grade Tracker</h1>
            <p>Lesson 1: Arrays vs Lists - When Each Makes Sense</p>
        </div>
        
        <div class='comparison-grid'>
            <div class='card array-card'>
                <div class='card-title'>📚 Array (Fixed Size)</div>
                <div class='items-container'>
                    {string.Join("", Array.ConvertAll(array, item => $"<div class='item array-item'>{item}</div>"))}
                </div>
                <div class='stats'>
                    <div class='stat-row'>
                        <span class='stat-label'>Size:</span>
                        <span class='stat-value'>{arraySize} (Fixed)</span>
                    </div>
                    <div class='stat-row'>
                        <span class='stat-label'>Type:</span>
                        <span class='stat-value'>string[]</span>
                    </div>
                    <div class='stat-row'>
                        <span class='stat-label'>Can grow:</span>
                        <span class='stat-value'>❌ No</span>
                    </div>
                </div>
            </div>
            
            <div class='card list-card'>
                <div class='card-title'>📋 List (Dynamic Size)</div>
                <div class='items-container'>
                    {string.Join("", list.ConvertAll(item => $"<div class='item list-item'>{item}</div>"))}
                </div>
                <div class='stats'>
                    <div class='stat-row'>
                        <span class='stat-label'>Size:</span>
                        <span class='stat-value'>{list.Count} (Dynamic)</span>
                    </div>
                    <div class='stat-row'>
                        <span class='stat-label'>Type:</span>
                        <span class='stat-value'>List&lt;string&gt;</span>
                    </div>
                    <div class='stat-row'>
                        <span class='stat-label'>Can grow:</span>
                        <span class='stat-value'>✅ Yes</span>
                    </div>
                </div>
            </div>
        </div>
        
        <div class='progress-section'>
            <div class='progress-title'>📈 List Growth Demonstration</div>
            <div class='progress-steps'>
                <div class='step'>
                    <div class='step-number'>1</div>
                    <div class='step-label'>Initial Count</div>
                    <div class='step-value'>{listInitialCount}</div>
                </div>
                <div class='arrow'>→</div>
                <div class='step'>
                    <div class='step-number'>2</div>
                    <div class='step-label'>After Adding 3</div>
                    <div class='step-value'>{listAfterAdding}</div>
                </div>
                <div class='arrow'>→</div>
                <div class='step'>
                    <div class='step-number'>3</div>
                    <div class='step-label'>After Adding 2 More</div>
                    <div class='step-value'>{listFinalCount}</div>
                </div>
            </div>
            
            <div class='highlight'>
                <strong>🎯 Key Takeaway:</strong> Lists can grow dynamically while arrays have a fixed size. 
                Use arrays when you know the exact size, use lists when you need flexibility!
            </div>
        </div>
    </div>
</body>
</html>";
    }

    public static void Stop()
    {
        listener?.Stop();
        listener?.Close();
        listener = null;
    }
}
