using System.Collections.Generic;

namespace Компилятор
{
    public class Keywords
    {
        public Dictionary<byte, Dictionary<string, byte>> KeywordMap { get; private set; }

        public Keywords()
        {
            var map = new Dictionary<byte, Dictionary<string, byte>>();

            var group = new Dictionary<string, byte>
            {
                ["do"] = LexicalAnalyzer.dosy,
                ["if"] = LexicalAnalyzer.ifsy,
                ["in"] = LexicalAnalyzer.insy,
                ["of"] = LexicalAnalyzer.ofsy,
                ["or"] = LexicalAnalyzer.orsy,
                ["to"] = LexicalAnalyzer.tosy
            };
            map[2] = group;

            group = new Dictionary<string, byte>
            {
                ["end"] = LexicalAnalyzer.endsy,
                ["var"] = LexicalAnalyzer.varsy,
                ["div"] = LexicalAnalyzer.divsy,
                ["and"] = LexicalAnalyzer.andsy,
                ["not"] = LexicalAnalyzer.notsy,
                ["for"] = LexicalAnalyzer.forsy,
                ["mod"] = LexicalAnalyzer.modsy,
                ["nil"] = LexicalAnalyzer.nilsy,
                ["set"] = LexicalAnalyzer.setsy
            };
            map[3] = group;

            // ... остальные группы ...
            group = new Dictionary<string, byte>
            {
                ["then"] = LexicalAnalyzer.thensy,
                ["else"] = LexicalAnalyzer.elsesy,
                ["case"] = LexicalAnalyzer.casesy,
                ["file"] = LexicalAnalyzer.filesy,
                ["goto"] = LexicalAnalyzer.gotosy,
                ["type"] = LexicalAnalyzer.typesy,
                ["with"] = LexicalAnalyzer.withsy
            };
            map[4] = group;

            group = new Dictionary<string, byte>
            {
                ["begin"] = LexicalAnalyzer.beginsy,
                ["while"] = LexicalAnalyzer.whilesy,
                ["array"] = LexicalAnalyzer.arraysy,
                ["const"] = LexicalAnalyzer.constsy,
                ["label"] = LexicalAnalyzer.labelsy,
                ["until"] = LexicalAnalyzer.untilsy
            };
            map[5] = group;

            group = new Dictionary<string, byte>
            {
                ["downto"] = LexicalAnalyzer.downtosy,
                ["packed"] = LexicalAnalyzer.packedsy,
                ["record"] = LexicalAnalyzer.recordsy,
                ["repeat"] = LexicalAnalyzer.repeatsy
            };
            map[6] = group;

            group = new Dictionary<string, byte> { ["program"] = LexicalAnalyzer.programsy };
            map[7] = group;

            group = new Dictionary<string, byte> { ["function"] = LexicalAnalyzer.functionsy };
            map[8] = group;

            group = new Dictionary<string, byte> { ["procedure"] = LexicalAnalyzer.procedurensy };
            map[9] = group;

            KeywordMap = map;
        }
    }
}