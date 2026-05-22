using System;
using System.Collections.Generic;
using System.IO;

namespace Компилятор
{
    public struct TextPosition
    {
        public uint LineNumber { get; set; }
        public byte CharNumber { get; set; }

        public TextPosition(uint ln = 0, byte c = 0)
        {
            LineNumber = ln;
            CharNumber = c;
        }
    }

    public struct Err
    {
        public TextPosition ErrorPosition { get; set; }
        public byte ErrorCode { get; set; }

        public Err(TextPosition pos, byte code)
        {
            ErrorPosition = pos;
            ErrorCode = code;
        }
    }

    public static class InputOutput
    {
        private const byte ErrMax = 9;

        public static char Ch { get; set; }
        public static TextPosition PositionNow { get; set; } = new TextPosition();
        public static List<Err>? Errors { get; private set; }

        private static string? _line;
        private static int _lastInLine = -1;
        private static StreamReader? _file;
        private static uint _errCount;
        private static bool _endReached;

        public static void Init(string filename)
        {
            _file = new StreamReader(filename);
            PositionNow = new TextPosition(1, 0);
            _endReached = false;
            ReadNextLine();
        }

        private static void ReadNextLine()
        {
            if (_file == null || _file.EndOfStream)
            {
                _endReached = true;
                _line = null;
                return;
            }

            _line = _file.ReadLine();
            if (_line == null)
            {
                _endReached = true;
                return;
            }

            Errors = new List<Err>();
            _lastInLine = _line.Length - 1;
        }

        private static void ListThisLine()
        {
            if (_line != null)
                Console.WriteLine(_line);
        }

        private static void ListErrors()
        {
            if (Errors == null) return;

            int pos = 6 - $"{PositionNow.LineNumber} ".Length;
            string s;

            foreach (Err item in Errors)
            {
                ++_errCount;
                s = "**";
                if (_errCount < 10) s += "0";
                s += $"{_errCount}**";

                while (s.Length - 1 < pos + item.ErrorPosition.CharNumber)
                    s += " ";

                s += $"^ ошибка код {item.ErrorCode}";
                Console.WriteLine(s);
            }
        }

        private static void End()
        {
            if (!_endReached)
            {
                _endReached = true;
                Console.WriteLine($"Компиляция завершена: ошибок — {_errCount}!");
                _file?.Close();
            }
        }

        public static void Error(byte errorCode, TextPosition position)
        {
            if (Errors == null) Errors = new List<Err>();
            if (Errors.Count <= ErrMax)
                Errors.Add(new Err(position, errorCode));
        }

        public static bool NextCh()
        {
            if (_endReached) return false;

            if (PositionNow.CharNumber > _lastInLine)
            {
                ListThisLine();
                if (Errors != null && Errors.Count > 0)
                    ListErrors();

                ReadNextLine();

                if (_endReached)
                {
                    End();
                    return false;
                }

                PositionNow = new TextPosition(PositionNow.LineNumber + 1, 0);

                // пустая строка – читаем следующую
                if (_lastInLine < 0) 
                    return NextCh();

                var currentPos = PositionNow;
                Ch = _line![currentPos.CharNumber];
                currentPos.CharNumber++;
                PositionNow = currentPos;
            }
            else
            {
                var currentPos = PositionNow;
                Ch = _line![currentPos.CharNumber];
                currentPos.CharNumber++;
                PositionNow = currentPos;
            }

            return true;
        }
    }
}