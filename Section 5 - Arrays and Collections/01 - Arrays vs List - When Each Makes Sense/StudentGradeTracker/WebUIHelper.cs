using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public static class WebUIHelper
{
    private static HttpListener? listener;
    private static string currentHtml = "";
    private static readonly int port = 4444;
    private static bool serverStarted = false;
    
    // Flexible method that can handle different lesson scenarios
    public static void Show(
        string lessonTitle = "Student Grade Tracker",
        string[] subjects = null,
        List<string> students = null,
        Dictionary<string, List<int>> grades = null,
        string analysisText = null)
    {
        Console.WriteLine($"🎓 {lessonTitle}");
        Console.WriteLine(new string('=', lessonTitle.Length + 4));
        Console.WriteLine();
        
        // Show basic analysis if provided
        if (!string.IsNullOrEmpty(analysisText))
        {
            Console.WriteLine("📊 Analysis Results:");
            Console.WriteLine($"   {analysisText}");
            Console.WriteLine();
        }
        
        try
        {
            StartServer();
            var html = GenerateHTML(lessonTitle, subjects, students, grades, analysisText);
            currentHtml = html;
            
            Console.WriteLine($"🌐 Web UI available at: http://localhost:{port}");
            Console.WriteLine("📱 Opening browser...");
            
            // Open browser with retry
            OpenBrowser();
            
            Console.WriteLine("✅ Web interface ready! Press any key when done viewing...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Web UI Error: {ex.Message}");
            Console.WriteLine("📋 Showing data in console instead:");
            ShowConsoleVersion(subjects, students, grades, analysisText);
        }
    }
    
    private static void StartServer()
    {
        if (serverStarted) return;
        
        try
        {
            listener = new HttpListener();
            listener.Prefixes.Add($"http://localhost:{port}/");
            listener.Start();
            serverStarted = true;
            
            // Handle requests in background
            Task.Run(HandleRequests);
        }
        catch (Exception ex)
        {
            throw new Exception($"Could not start web server on port {port}: {ex.Message}");
        }
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
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                
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
    
    private static void OpenBrowser()
    {
        try
        {
            var url = $"http://localhost:{port}";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception)
        {
            // Try alternative method
            try
            {
                Process.Start("cmd", $"/c start http://localhost:{port}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Could not open browser automatically: {ex.Message}");
                Console.WriteLine($"   Please manually open: http://localhost:{port}");
            }
        }
    }
    
    public static void Stop()
    {
        try
        {
            listener?.Stop();
            listener?.Close();
            listener = null;
            serverStarted = false;
            Console.WriteLine("🔄 Web UI server stopped.");
        }
        catch (Exception)
        {
            // Ignore cleanup errors
        }
    }
    
    // Legacy method for backwards compatibility
    public static void ShowArrayVsList(string[] array, List<string> list, 
        int arraySize, int listInitialCount, int listAfterAdding, int listFinalCount)
    {
        string analysisText = $"Array size (fixed): {arraySize}, List growth: {listInitialCount} → {listAfterAdding} → {listFinalCount}";
        Show("Lesson 1: Arrays vs Lists", array, list, null, analysisText);
        
        Console.WriteLine("📊 Analysis Results:");
        Console.WriteLine($"   Array size (fixed): {arraySize}");
        Console.WriteLine($"   List initial: {listInitialCount} → after adding: {listFinalCount}");
        Console.WriteLine();
        
        try
        {
            var html = GenerateArrayVsListHTML(array, list, arraySize, listInitialCount, listAfterAdding, listFinalCount);
            File.WriteAllText(outputPath, html);
            
            Console.WriteLine("🌐 Opening beautiful web interface...");
            
            // Open in browser
            Process.Start(new ProcessStartInfo
            {
                FileName = outputPath,
                UseShellExecute = true
            });
            
            Console.WriteLine("✅ Web interface opened! Press any key when done viewing...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
            Console.WriteLine("📋 Showing data in console instead:");
            ShowConsoleVersion(array, list, arraySize, listInitialCount, listAfterAdding, listFinalCount);
        }
    }
    
    private static void ShowConsoleVersion(string[] subjects, List<string> students, 
        Dictionary<string, List<int>> grades, string analysisText)
    {
        Console.WriteLine();
        
        if (subjects != null && subjects.Length > 0)
        {
            Console.WriteLine("📚 SUBJECTS (Array - Fixed Size):");
            foreach (var item in subjects)
                Console.WriteLine($"   • {item}");
            Console.WriteLine($"   Size: {subjects.Length} (cannot change)");
            Console.WriteLine();
        }
        
        if (students != null && students.Count > 0)
        {
            Console.WriteLine("📋 STUDENTS (List - Dynamic Size):");
            foreach (var item in students)
                Console.WriteLine($"   • {item}");
            Console.WriteLine($"   Count: {students.Count} (can grow/shrink)");
            Console.WriteLine();
        }
        
        if (grades != null && grades.Count > 0)
        {
            Console.WriteLine("📊 GRADES (Dictionary):");
            foreach (var kvp in grades)
            {
                Console.WriteLine($"   {kvp.Key}: [{string.Join(", ", kvp.Value)}]");
            }
            Console.WriteLine();
        }
        
        if (!string.IsNullOrEmpty(analysisText))
        {
            Console.WriteLine($"📈 Analysis: {analysisText}");
        }
    }
    
    private static string GenerateHTML(string lessonTitle, string[] subjects, List<string> students, 
        Dictionary<string, List<int>> grades, string analysisText)
    {
        var subjectsHtml = "";
        var studentsHtml = "";
        var gradesHtml = "";
        
        if (subjects != null && subjects.Length > 0)
        {
            subjectsHtml = $@"
                <div class='card array-card'>
                    <div class='card-title'>📚 Subjects (Array - Fixed Size)</div>
                    <div class='items-container'>
                        {string.Join("", Array.ConvertAll(subjects, item => $"<div class='item array-item'>{item}</div>"))}
                    </div>
                    <div class='stats'>
                        <div class='stat-row'>
                            <span class='stat-label'>Size:</span>
                            <span class='stat-value'>{subjects.Length} (Fixed)</span>
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
                </div>";
        }
        
        if (students != null && students.Count > 0)
        {
            studentsHtml = $@"
                <div class='card list-card'>
                    <div class='card-title'>📋 Students (List - Dynamic Size)</div>
                    <div class='items-container'>
                        {string.Join("", students.ConvertAll(item => $"<div class='item list-item'>{item}</div>"))}
                    </div>
                    <div class='stats'>
                        <div class='stat-row'>
                            <span class='stat-label'>Count:</span>
                            <span class='stat-value'>{students.Count} (Dynamic)</span>
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
                </div>";
        }
        
        if (grades != null && grades.Count > 0)
        {
            var gradeItems = "";
            foreach (var kvp in grades)
            {
                var avg = kvp.Value.Count > 0 ? kvp.Value.Sum() / (double)kvp.Value.Count : 0;
                gradeItems += $@"
                    <div class='grade-row'>
                        <span class='student-name'>{kvp.Key}</span>
                        <span class='grades'>[{string.Join(", ", kvp.Value)}]</span>
                        <span class='average'>{avg:F1}%</span>
                    </div>";
            }
            
            gradesHtml = $@"
                <div class='card grades-card'>
                    <div class='card-title'>📊 Grades (Dictionary)</div>
                    <div class='grades-container'>
                        {gradeItems}
                    </div>
                    <div class='stats'>
                        <div class='stat-row'>
                            <span class='stat-label'>Students with grades:</span>
                            <span class='stat-value'>{grades.Count}</span>
                        </div>
                        <div class='stat-row'>
                            <span class='stat-label'>Type:</span>
                            <span class='stat-value'>Dictionary&lt;string, List&lt;int&gt;&gt;</span>
                        </div>
                    </div>
                </div>";
        }
        
        var analysisHtml = "";
        if (!string.IsNullOrEmpty(analysisText))
        {
            analysisHtml = $@"
                <div class='highlight'>
                    <strong>📈 Analysis:</strong> {analysisText}
                </div>";
        }
        
        var gridClass = "";
        var cardCount = 0;
        if (!string.IsNullOrEmpty(subjectsHtml)) cardCount++;
        if (!string.IsNullOrEmpty(studentsHtml)) cardCount++;
        if (!string.IsNullOrEmpty(gradesHtml)) cardCount++;
        
        gridClass = cardCount switch
        {
            1 => "single-card",
            2 => "two-cards", 
            3 => "three-cards",
            _ => "comparison-grid"
        };
        
        return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>{lessonTitle}</title>
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
        
        .comparison-grid, .single-card, .two-cards, .three-cards {{
            display: grid;
            gap: 30px;
            margin-bottom: 40px;
        }}
        
        .comparison-grid, .two-cards {{ grid-template-columns: 1fr 1fr; }}
        .three-cards {{ grid-template-columns: repeat(3, 1fr); }}
        .single-card {{ grid-template-columns: 1fr; max-width: 600px; margin: 0 auto; }}
        
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
            font-size: 1.8rem;
            margin-bottom: 20px;
            text-align: center;
            font-weight: 600;
        }}
        
        .array-card .card-title {{ color: #F7931E; }}
        .list-card .card-title {{ color: #16C60C; }}
        .grades-card .card-title {{ color: #E91E63; }}
        
        .items-container {{
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            margin-bottom: 20px;
        }}
        
        .item {{
            color: white;
            padding: 8px 16px;
            border-radius: 20px;
            font-size: 0.9rem;
            font-weight: 500;
            animation: popIn 0.5s ease-out;
            animation-fill-mode: both;
        }}
        
        .array-item {{ background: linear-gradient(45deg, #F7931E, #FF6B35); }}
        .list-item {{ background: linear-gradient(45deg, #16C60C, #4CAF50); }}
        
        .grades-container {{
            margin-bottom: 20px;
        }}
        
        .grade-row {{
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 10px;
            margin-bottom: 8px;
            background: rgba(255, 255, 255, 0.05);
            border-radius: 8px;
        }}
        
        .student-name {{ font-weight: 600; color: #E91E63; }}
        .grades {{ color: #CCCCCC; font-family: monospace; }}
        .average {{ color: #16C60C; font-weight: bold; }}
        
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
        
        .stat-label {{ color: #CCCCCC; }}
        .stat-value {{ color: #FFFFFF; font-weight: 600; }}
        
        .highlight {{
            background: rgba(81, 43, 212, 0.1);
            border-left: 4px solid #512BD4;
            padding: 15px;
            border-radius: 5px;
            margin: 20px 0;
            animation: slideInUp 1.2s ease-out;
        }}
        
        @keyframes fadeInDown {{
            from {{ opacity: 0; transform: translateY(-30px); }}
            to {{ opacity: 1; transform: translateY(0); }}
        }}
        
        @keyframes slideInUp {{
            from {{ opacity: 0; transform: translateY(50px); }}
            to {{ opacity: 1; transform: translateY(0); }}
        }}
        
        @keyframes popIn {{
            from {{ opacity: 0; transform: scale(0.8); }}
            to {{ opacity: 1; transform: scale(1); }}
        }}
        
        @media (max-width: 768px) {{
            .comparison-grid, .two-cards, .three-cards {{
                grid-template-columns: 1fr;
            }}
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🎓 {lessonTitle}</h1>
        </div>
        
        <div class='{gridClass}'>
            {subjectsHtml}
            {studentsHtml}
            {gradesHtml}
        </div>
        
        {analysisHtml}
    </div>
</body>
</html>";
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
}
