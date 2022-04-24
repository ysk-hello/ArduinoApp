using Iot.Device.Arduino;
using System.Device.Gpio;
using UnitsNet;

const int BUTTON_PIN = 2;
const int LED_PIN = 13;
const int SENSOR_PIN = 14;  // A0

// ls -l /dev/serial/by-id/
const string PORT_NAME = "../../dev/ttyACM1";
const int BAUD_RATE = 57600;

var board = new ArduinoBoard(PORT_NAME, BAUD_RATE);

// キー入力待ち
while (Menu(board))
{
}

board?.Dispose();

static bool Menu(ArduinoBoard board)
{
    Console.WriteLine($"Select the test you want to run:");
    Console.WriteLine($" 1 Run GPIO tests with a simple led blinking on GPIO{LED_PIN} port");;
    Console.WriteLine($" 2 Run callback event test on GPIO{BUTTON_PIN}");
    Console.WriteLine($" 3 Read analog channel as fast as possible");
    Console.WriteLine($" X Exit");
    var key = Console.ReadKey();
    Console.WriteLine();

    switch (key.KeyChar)
    {
        case '1':
            TestGpio(board);
            break;
        case '2':
            TestEventsCallback(board);
            break;
        case '3':
            TestAnalogCallback(board);
            break;
        case 'x':
        case 'X':
            return false;
    }

    return true;
}

/// <summary>
/// LEDを点滅させます。
/// </summary>
static void TestGpio(ArduinoBoard board)
{
    var gpioController = board.CreateGpioController();

    // Opening GPIO
    gpioController.OpenPin(LED_PIN);
    gpioController.SetPinMode(LED_PIN, PinMode.Output);

    Console.WriteLine($"Blinking GPIO{LED_PIN}");
    while (!Console.KeyAvailable)
    {
        gpioController.Write(LED_PIN, PinValue.High);   // 点灯
        Thread.Sleep(500);
        gpioController.Write(LED_PIN, PinValue.Low);    // 消灯
        Thread.Sleep(500);
    }

    Console.ReadKey();
    gpioController.Dispose();
}

/// <summary>
/// ボタンの状態に応じて、LEDを点滅させます。
/// </summary>
static void TestEventsCallback(ArduinoBoard board)
{
    throw new NotSupportedException();
}

/// <summary>
/// 温度センサから取得した値から温度を計算し、出力します。
/// </summary>
static void TestAnalogCallback(ArduinoBoard board)
{
    throw new NotSupportedException();
}
