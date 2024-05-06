using UnityEngine;
using VContainer;

namespace Source
{
    public sealed class Logger
    {
        public void Log(string message) => Debug.Log("[Logger] " + message);
    }

    public sealed class Calculator
    {
        public int Add(int a, int b) => a + b;
    }

    public sealed class HogeClass
    {
        private readonly Logger _logger;
        private readonly Calculator _calculator;

        [Inject]
        public HogeClass(Logger logger, Calculator calculator)
        {
            _logger = logger;
            _calculator = calculator;
        }

        public void LoggerTest()
        {
            _logger.Log("LoggerTest");
        }

        //Advise: CalculatorTestは名詞（修飾語＋名詞）になってない？メソッドは動詞から始まるように
        public void CalculatorTest(int a, int b)
        {
            int result = _calculator.Add(a, b);
            _logger.Log($"{a} + {b} = {result}");
        }
    }

}