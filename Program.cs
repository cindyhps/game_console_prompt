using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        string[] menuItems = { "Mulai Game", "Keluar" };
        int selectedItemIndex = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("************************************************************");
            Console.WriteLine("*                    [Main Menu]                           *");
            Console.WriteLine("*                                                          *");
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedItemIndex)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($"                {i + 1}. {menuItems[i]}");
                Console.ResetColor();
            }
            Console.WriteLine("*                                                          *");
            Console.WriteLine("************************************************************");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                selectedItemIndex = (selectedItemIndex - 1 + menuItems.Length) % menuItems.Length;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                selectedItemIndex = (selectedItemIndex + 1) % menuItems.Length;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                if (selectedItemIndex == 0)
                {
                    // Panggil method untuk memulai game
                    ShowLoadingScreen();
                    //ShowIntro();
                }
                else if (selectedItemIndex == 1)
                {
                    // Keluar dari game
                    Environment.Exit(0);
                }
            }
        }
    }

    static void ShowLoadingScreen()
    {
        Console.Clear();
        Console.WriteLine("[Loading ...]");
        Console.WriteLine("Tekan Enter untuk melanjutkan.");

        // Menunggu user menekan Enter
        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }

        // Setelah user menekan Enter, panggil metode InGame
        Prolog();
    }

    static void Prolog()
    {
        Console.CursorVisible = false;
        Console.WriteLine("Selamat Datang.");
        Console.ReadLine();

        ShowDialog();
    }

    static void ShowDialog()
    {
        Console.Clear();
        Console.WriteLine("[InGame]");
        Console.WriteLine();

        // Teks dialog
        string[] dialogLines = {
            //"Terdapat pemandangan berantakan dalam ruanganmu.",
            "Kamar yang luas.",
            //"Dikarenakan penuh barang yang berserakan dilantai,",
            //"Membuatnya terlihat kamar tersebut begitu penuh.",
            //"Di sana terdapat komputermu yang sudah hidup beberapa waktu yang lalu.",
            //"“Gabut rasanya”",
            //"“Game apa yang bisa kumainkan hari ini”",
            //"“Aku ingin memainkan game jadul”",
        };

        int startIndex = 0;
        int endIndex = 0;

        while (endIndex < dialogLines.Length)
        {
            endIndex = Math.Min(startIndex + 5, dialogLines.Length);
            DisplayText(dialogLines, startIndex, endIndex);
            Console.WriteLine("Tekan Enter untuk melanjutkan...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("[Game]");
            Console.WriteLine();
            startIndex = endIndex;
        }

        ShowOptions();
    }

    static void DisplayText(string[] dialogLines, int startIndex, int endIndex)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            Console.Write("> ");
            ShowTextPerSentence(dialogLines[i]);
            Console.WriteLine();
        }
    }

    static void ShowTextPerSentence(string sentence)
    {
        int currentLength = 0;
        for (int i = 0; i < sentence.Length; i++)
        {
            Console.Write(sentence[i]);
            Thread.Sleep(20); // Jeda 0.02 detik per huruf

            if (sentence[i] == '.' || sentence[i] == '!' || sentence[i] == '?')
            {
                currentLength++;
                if (currentLength == 5)
                {
                    Console.WriteLine();
                    return;
                }
                Console.ReadLine(); // Tunggu tekanan Enter untuk lanjut ke kalimat berikutnya
            }
        }
    }

    static void DisplayTextInGame(string[] dialogLines, int startIndex, int endIndex)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            Console.Write("> ");
            ShowTextPerSentence(dialogLines[i]);
            Console.WriteLine();
        }
    }

    static void ShowOptions()
    {
        string[] options = { "Tetris", "Batu Gunting Kertas", "Cooming Soon" };
        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Pilih salah satu game mu:");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    if (i == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta; // Warna ungu untuk Tetris
                    }
                    else if (i == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Warna merah untuk Bunga Mawar
                    }
                    else if (i == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; // Warna kuning untuk Bunga Krisan
                    }

                    Console.WriteLine($"[{options[i]}]");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(options[i]);
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex + 1) % options.Length;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine($"\nAnda memilih: {options[selectedIndex]}");

                // Panggil metode untuk menampilkan dialog sesuai dengan pilihan
                switch (selectedIndex)
                {
                    case 0:
                        StartTetris();
                        break;
                    case 1:
                        StartBatuGuntingKertas();
                        break;
                    case 2:
                        // Panggil metode untuk game ketiga
                        break;
                }

                break;
            }
        }
    }

    static void StartTetris()
    {
        TetrisGame tetrisGame = new TetrisGame();
        tetrisGame.Run();
    }

    static void StartBatuGuntingKertas()
    {
        Console.WriteLine("Selamat datang di permainan Batu Gunting Kertas!");
        Console.WriteLine("Pilih: (1) Batu, (2) Gunting, (3) Kertas");
        Console.Write("Pilihan Anda: ");

        // Membaca input dari pengguna
        int playerChoice;
        while (!int.TryParse(Console.ReadLine(), out playerChoice) || playerChoice < 1 || playerChoice > 3)
        {
            Console.WriteLine("Masukan tidak valid. Pilih 1 untuk Batu, 2 untuk Gunting, atau 3 untuk Kertas.");
            Console.Write("Pilihan Anda: ");
        }

        // Mengkonversi pilihan pengguna ke string
        string[] choices = { "Batu", "Gunting", "Kertas" };
        string playerChoiceString = choices[playerChoice - 1];

        // Generate pilihan komputer secara acak
        Random random = new Random();
        int computerChoice = random.Next(1, 4);
        string computerChoiceString = choices[computerChoice - 1];

        // Menampilkan pilihan pengguna dan komputer
        Console.WriteLine($"Anda memilih: {playerChoiceString}");
        Console.WriteLine($"Komputer memilih: {computerChoiceString}");

        // Menentukan pemenang
        if (playerChoice == computerChoice)
        {
            Console.WriteLine("Hasilnya Seri!");
        }
        else if ((playerChoice == 1 && computerChoice == 2) || (playerChoice == 2 && computerChoice == 3) || (playerChoice == 3 && computerChoice == 1))
        {
            Console.WriteLine("Anda Menang!");
        }
        else
        {
            Console.WriteLine("Komputer Menang!");
        }

        Console.WriteLine("Terima kasih telah bermain!");
    }

}

public class TetrisGame
{
    private const int Width = 10;
    private const int Height = 20;

    private bool[,] grid = new bool[Width, Height];
    private Block currentBlock;
    private Random random = new Random();
    private int score = 0;
    private bool isGameOver = false;
    private bool isPaused = false;


    public void Run()
    {
        int randomX = random.Next(0, Width); // Deklarasi randomX di sini
        currentBlock = GenerateNewBlock(randomX);

        while (!isGameOver)
        {
            Console.Clear(); // Clear the screen before drawing
            Draw(); // Draw the game grid
            ProcessInput();
            Update();
            Thread.Sleep(500); // Mengubah kecepatan turun menjadi lebih lambat
        }

        Console.Clear(); // Clear the screen before displaying Game Over
        Draw(); // Draw the game grid one last time before Game Over
        Console.WriteLine("Game Over!");
        Console.WriteLine("Score: " + score);
        Console.WriteLine("Tekan Enter untuk kembali ke menu...");
        Console.ReadLine(); // Wait for Enter key press before returning to the menu
    }

    private Block GenerateNewBlock(int randomX)
    {
        return new Block(randomX, 0); // Set Y coordinate to 0 to always start from the top
    }


    public bool CanMoveDown(bool[,] grid)
    {
        for (int i = 0; i < this.currentBlock.Shape.GetLength(1); i++)
        {
            for (int j = this.currentBlock.Shape.GetLength(0) - 1; j >= 0; j--)
            {
                if (this.currentBlock.Shape[j, i] == 1)
                {
                    if (this.currentBlock.Y + j + 1 >= Height || grid[this.currentBlock.X + i, this.currentBlock.Y + j + 1])
                        return false;
                }
            }
        }
        return true;
    }


    private void Draw()
    {
        Console.WriteLine("Score: " + score);
        Console.WriteLine(new string('-', Width * 2 + 2));

        for (int y = 0; y < Height; y++)
        {
            Console.Write("|");
            for (int x = 0; x < Width; x++)
            {
                if (grid[x, y])
                {
                    Console.Write("[]");
                }
                else if (currentBlock != null && currentBlock.IsBlockAtPosition(x, y))
                {
                    Console.Write("c ");
                }
                else
                {
                    Console.Write("  ");
                }
            }
            Console.WriteLine("|");
        }

        Console.WriteLine(new string('-', Width * 2 + 2));
    }


    private void ProcessInput()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Escape)
            {
                isPaused = true;
                ShowPauseMenu();
            }
            else if (!isPaused) // Tambahkan kondisi untuk mengabaikan input saat game sedang di-pause
            {
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (currentBlock != null) currentBlock.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentBlock != null) currentBlock.MoveRight();
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentBlock != null) currentBlock.MoveDown();
                        break;
                    case ConsoleKey.Enter:
                        if (currentBlock != null) currentBlock.Rotate();
                        break;
                }
            }
        }
    }


    private void Update()
    {
        if (!isPaused)
        {
            if (currentBlock == null || currentBlock.IsStopped(grid))
            {
                if (currentBlock == null)
                {
                    int randomX = random.Next(0, Width);
                    currentBlock = new Block(randomX, 0);
                }

                if (currentBlock.IsStopped(grid))
                {
                    currentBlock.Stop(grid);
                    UpdateGrid();
                    CheckLines();

                    // Check if any part of the block is at or below the bottom boundary of the grid
                    bool isAtOrBelowBottom = false;
                    for (int i = 0; i < currentBlock.Shape.GetLength(1); i++)
                    {
                        for (int j = 0; j < currentBlock.Shape.GetLength(0); j++)
                        {
                            if (currentBlock.Shape[j, i] == 1 && currentBlock.Y + j >= Height)
                            {
                                isAtOrBelowBottom = true;
                                break;
                            }
                        }
                        if (isAtOrBelowBottom) break;
                    }

                    // If any part of the block is at or below the bottom boundary, end the game
                    if (isAtOrBelowBottom)
                    {
                        isGameOver = true;
                        return;
                    }

                    // Generate a new block
                    int randomX = random.Next(0, Width);
                    currentBlock = new Block(randomX, 0);

                    // Check if the new block is valid
                    if (!currentBlock.IsValid(grid))
                    {
                        isGameOver = true;
                        return;
                    }
                }
            }
            else if (currentBlock.CanMoveDown(grid))
            {
                currentBlock.MoveDown();
            }
        }
    }


    private void GameOver()
    {
        // No changes needed here
        isGameOver = true;
    }


    private void UpdateGrid()
    {
        for (int i = 0; i < currentBlock.Shape.GetLength(1); i++)
        {
            for (int j = 0; j < currentBlock.Shape.GetLength(0); j++)
            {
                if (currentBlock.Shape[j, i] == 1)
                {
                    grid[currentBlock.X + i, currentBlock.Y + j] = true;
                }
            }
        }
    }


    private void CheckLines()
    {
        int linesCleared = 0;
        for (int y = Height - 1; y >= 0; y--)
        {
            bool isLineFull = true;
            for (int x = 0; x < Width; x++)
            {
                if (!grid[x, y])
                {
                    isLineFull = false;
                    break;
                }
            }
            if (isLineFull)
            {
                // Hapus baris yang penuh
                for (int yy = y; yy > 0; yy--)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        grid[x, yy] = grid[x, yy - 1];
                    }
                }
                linesCleared++;

                // Tambahkan score berdasarkan jumlah baris yang berhasil dihapus
                score += 100;

                // Check if there are blocks above the cleared line and move them down
                for (int i = y - 1; i >= 0; i--)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        grid[x, i + 1] = grid[x, i];
                    }
                }
            }
        }
    }


    private void ShowPauseMenu()
    {
        while (isPaused)
        {
            Console.Clear();
            Console.WriteLine("[Pause]");
            Console.WriteLine("1. Lanjutkan Game");
            Console.WriteLine("2. Keluar dari Game");
            Console.WriteLine("Pilih opsi (1/2):");

            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    isPaused = false;
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    isGameOver = true;
                    Console.WriteLine("Keluar dari Game.");
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid. Silakan pilih lagi.");
                    break;
            }
        }
    }
}

public class Block
{
    private int x, y;
    private int[,] shape;
    private int rotation = 0;
    private Random random = new Random();

    public Block(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.shape = GenerateShape();
    }

    public int[,] Shape { get { return shape; } }
    public int X { get { return x; } }
    public int Y { get { return y; } }

    public bool IsBlockAtPosition(int posX, int posY)
    {
        int relativeX = posX - x;
        int relativeY = posY - y;

        if (relativeX >= 0 && relativeX < shape.GetLength(1) &&
            relativeY >= 0 && relativeY < shape.GetLength(0))
        {
            return shape[relativeY, relativeX] == 1;
        }

        return false;
    }

    public void MoveLeft()
    {
        if (x > 0)
            x--;
    }

    public void MoveRight()
    {
        if (x + shape.GetLength(1) < 10)
            x++;
    }

    public void MoveDown()
    {
        y++;
    }

    public bool CanMoveDown(bool[,] grid)
    {
        for (int i = 0; i < shape.GetLength(1); i++)
        {
            for (int j = shape.GetLength(0) - 1; j >= 0; j--)
            {
                if (shape[j, i] == 1)
                {
                    if (y + j + 1 >= 20 || grid[x + i, y + j + 1])
                        return false;
                }
            }
        }
        return true;
    }

    public void Stop(bool[,] grid)
    {
        for (int i = 0; i < shape.GetLength(1); i++)
        {
            for (int j = 0; j < shape.GetLength(0); j++)
            {
                if (shape[j, i] == 1)
                    grid[x + i, y + j] = true;
            }
        }
    }

    public bool IsStopped(bool[,] grid)
    {
        // Check if block cannot move down anymore
        return !CanMoveDown(grid);
    }

    public void Rotate()
    {
        rotation = (rotation + 1) % 4;
        int[,] newShape = new int[shape.GetLength(1), shape.GetLength(0)];

        for (int i = 0; i < shape.GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetLength(1); j++)
            {
                newShape[j, shape.GetLength(0) - i - 1] = shape[i, j];
            }
        }

        shape = newShape;
    }

    public bool IsValid(bool[,] grid)
    {
        for (int i = 0; i < this.shape.GetLength(1); i++)
        {
            for (int j = 0; j < this.shape.GetLength(0); j++)
            {
                // Perhitungan koordinat yang benar
                int currentX = this.x + i;
                int currentY = this.y + j;

                // Periksa apakah posisi koordinat berada dalam batas array
                if (currentX < 0 || currentX >= grid.GetLength(0) || currentY < 0 || currentY >= grid.GetLength(1))
                {
                    return false;
                }

                if (this.shape[j, i] == 1 && grid[currentX, currentY])
                    return false;
            }
        }
        return true;
    }

    private int[,] GenerateShape()
    {
        // Define the shapes of different tetrominoes as jagged array
        int[][][] shapes = {
        new int[][] {
            new int[] { 1, 1 },
            new int[] { 1, 1 }
        },
        new int[][] {
            new int[] { 1, 0, 0 },
            new int[] { 1, 0, 0 },
            new int[] { 1, 1, 0 }
        },
        new int[][] {
            new int[] { 0, 1, 0 },
            new int[] { 0, 1, 0 },
            new int[] { 1, 1, 0 }
        },
        new int[][] {
            new int[] { 0, 1 },
            new int[] { 1, 1 },
            new int[] { 1, 0 }
        },
        new int[][] {
            new int[] { 1, 1, 1, 1 }
        }
    };

        // Select a random shape
        int[][] selectedShape = shapes[random.Next(0, shapes.Length)];

        // Determine the maximum height and width of the shape
        int maxHeight = selectedShape.Length;
        int maxWidth = selectedShape[0].Length;

        // Create a 2D array with dimensions to accommodate the selected shape
        int[,] shapeArray = new int[maxHeight, maxWidth];

        // Copy the values from the selected shape to the new 2D array
        for (int i = 0; i < maxHeight; i++)
        {
            for (int j = 0; j < maxWidth; j++)
            {
                shapeArray[i, j] = selectedShape[i][j];
            }
        }

        return shapeArray;
    }
}