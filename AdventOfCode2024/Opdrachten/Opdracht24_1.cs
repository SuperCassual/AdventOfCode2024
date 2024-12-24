using System.Xml.Linq;

namespace AdventOfCode2024;

class Opdracht24_1 : IOpdracht
{
    List<LogicNode> nodes;
    List<LogicGate> gates;
    List<string> wrongGateNames;

    public void Run()
    {
        nodes = new List<LogicNode>();
        gates = new List<LogicGate>();

        StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O24-1.txt");
        string line = sr.ReadLine();
        while (line != null && line != "")
        {
            string[] lines = line.Split(' ');
            nodes.Add(new LogicNode(lines[0].Substring(0, 3), lines[1] == "1"));
            line = sr.ReadLine();
        }
        line = sr.ReadLine();
        while (line != null && line != "")
        {
            string[] lines = line.Split(" ");
            LogicNode inputNode1 = FindOrMakeNode(lines[0]);
            LogicNode inputNode2 = FindOrMakeNode(lines[2]);
            LogicNode outputNode = FindOrMakeNode(lines[4]);
            LogicGate newGate = new LogicGate(inputNode1, inputNode2, outputNode, lines[1]);
            gates.Add(newGate);
            line = sr.ReadLine();
        }

        Part1(new List<LogicGate>(gates));
        Part2();
    }

    private void Part2()
    {
        wrongGateNames = new List<string>();
        LogicGate overflow = null;
        for (int i = 0; i < 45; i++)
        {
            if (i == 0)
            {
                overflow = CheckHalfAdders(i);
                continue;
            }
            overflow = CheckFullAdders(i, overflow);
        }
        wrongGateNames.Sort();
        string results = "";
        foreach (string gateName in wrongGateNames)
        {
            results += gateName;
            results += ',';
        }
        Console.WriteLine(results.TrimEnd(','));
    }

    private LogicGate CheckHalfAdders(int i)
    {
        string itos = i.ToString("D2");
        LogicGate lowAdderXorGate = gates.Find(item => DoesGateHaveInput(item, 'x' + itos) && item.Operation == "XOR");
        LogicGate lowAdderAndGate = gates.Find(item => DoesGateHaveInput(item, 'x' + itos) && item.Operation == "AND");

        LogicGate zGate = gates.Find(item => item.Output.Name == 'z' + itos);
        if (!DoesGateHaveInputs(zGate, 'x' + itos, 'y' + itos))
        {
            Console.WriteLine($"ISSUE! z{itos} is connected to {zGate.Input1} and {zGate.Input2} but should be connected to x{itos} and y{itos}");
        }
        return lowAdderAndGate;
    }

    private bool DoesGateHaveInputs(LogicGate gate, LogicNode input1, LogicNode input2)
    {
        return DoesGateHaveInput(gate, input1) && DoesGateHaveInput(gate, input2);
    }

    private bool DoesGateHaveInput(LogicGate gate, LogicNode input1)
    {
        return gate.Input1 == input1 || gate.Input2 == input1;
    }

    private bool DoesGateHaveInput(LogicGate gate, string input1)
    {
        LogicNode node1 = nodes.Find(node => node.Name == input1);
        if(node1 == null)
        {
            return false;
        }
        return DoesGateHaveInput(gate, node1);
    }

    private bool DoesGateHaveInputs(LogicGate gate, string input1, string input2)
    {
        LogicNode node1 = nodes.Find(node => node.Name == input1);
        LogicNode node2 = nodes.Find(node => node.Name == input2);
        if (node1 == null || node2 == null)
        {
            return false;
        }
        return DoesGateHaveInputs(gate, node1, node2);
    }

    private LogicGate CheckFullAdders(int i, LogicGate overflowGate)
    {
        string itos = i.ToString("D2");
        LogicGate lowAdderXorGate = gates.Find(item => DoesGateHaveInputs(item, 'x' + itos, 'y' + itos) && item.Operation == "XOR");
        LogicGate lowAdderAndGate = gates.Find(item => DoesGateHaveInputs(item, 'x' + itos, 'y' + itos) && item.Operation == "AND");

        LogicGate zGate = gates.Find(item => item.Output.Name == 'z' + itos);

        CheckZGate(overflowGate, lowAdderXorGate, zGate);

        LogicGate UpperoverflowGate = gates.Find(item => DoesGateHaveInputs(item, lowAdderXorGate.Output, overflowGate.Output) && item.Operation == "AND");
        LogicGate orGate = gates.Find(item => DoesGateHaveInputs(item, UpperoverflowGate.Output, lowAdderAndGate.Output) && item.Operation == "OR");
        orGate = CheckOrGate(lowAdderAndGate, UpperoverflowGate, orGate);

        return orGate;

    }

    private LogicGate CheckOrGate(LogicGate lowAdderAndGate, LogicGate UpperoverflowGate, LogicGate orGate)
    {
        if (orGate == null)
        {
            LogicGate tryGate1 = gates.Find(item => DoesGateHaveInput(item, UpperoverflowGate.Output) && item.Operation == "OR");
            if (tryGate1 != null)
            {
                WriteIssueSwap(tryGate1, lowAdderAndGate);
            }
            LogicGate tryGate2 = gates.Find(item => DoesGateHaveInput(item, lowAdderAndGate.Output) && item.Operation == "OR");
            if (tryGate2 != null)
            {
                WriteIssueSwap(tryGate2, UpperoverflowGate);
            }
            if (tryGate1 == null && tryGate2 == null)
            {
                Console.WriteLine("Apollo Guide Us");
            }
            orGate = gates.Find(item => DoesGateHaveInputs(item, UpperoverflowGate.Output, lowAdderAndGate.Output) && item.Operation == "OR");
        }
        return orGate;
    }

    private void CheckZGate(LogicGate overflowGate, LogicGate lowAdderXorGate, LogicGate zGate)
    {
        if (DoesGateHaveInputs(zGate, lowAdderXorGate.Output, overflowGate.Output) && zGate.Operation != "XOR")
        {
            LogicGate wrongGate = gates.Find(item => DoesGateHaveInputs(item, lowAdderXorGate.Output, overflowGate.Output) && item.Operation == "XOR");
            WriteIssueSwap(wrongGate, zGate);
        }

        if (!DoesGateHaveInputs(zGate, lowAdderXorGate.Output, overflowGate.Output))
        {
            LogicGate inputsGate = gates.Find(gate => DoesGateHaveInputs(gate, lowAdderXorGate.Output, overflowGate.Output) && gate.Operation == "XOR");
            if (inputsGate != null)
            {
                WriteIssueSwap(inputsGate, zGate);
            }
            else if (DoesGateHaveInput(zGate, overflowGate.Output))
            { //lowadderxorgate is wrong
                LogicGate wrongGate = gates.Find(item => item.Output == OtherInput(zGate, overflowGate.Output));
                WriteIssueSwap(lowAdderXorGate, wrongGate);
            }
            else if (DoesGateHaveInput(zGate, lowAdderXorGate.Output))
            {
                LogicGate wrongGate = gates.Find(item => item.Output == OtherInput(zGate, lowAdderXorGate.Output));
                WriteIssueSwap(wrongGate, overflowGate);
            }
            else
            { //both fucking inputs are wrong
                Console.WriteLine("God save us");
            }
        }
    }

    private LogicNode OtherInput(LogicGate gate, LogicNode input)
    {
        if (gate.Input1 == input)
        {
            return gate.Input2;
        }
        return gate.Input1;
    }

    private void MakeIssueSwap(LogicGate gate1, LogicGate gate2)
    {
        LogicNode output = gate1.Output;
        gate1.Output = gate2.Output;
        gate2.Output = output;
    }

    private void WriteIssueSwap(LogicGate gate1, LogicGate gate2)
    {
        wrongGateNames.Add(gate1.Output.Name);
        wrongGateNames.Add(gate2.Output.Name);
        Console.WriteLine($"ISSUE: \n{gate1.Input1.Name} {gate1.Operation} {gate1.Input2.Name} -> {gate1.Output.Name} and {gate2.Input1.Name} {gate2.Operation} {gate2.Input2.Name} -> {gate2.Output.Name} should be swapped ");
        MakeIssueSwap(gate1, gate2);
    }

    private void Part1(List<LogicGate> part1Gates)
    {
        int i = 0;
        while (!AllZsStated())
        {
            i %= part1Gates.Count;
            if (part1Gates[i].TryOperation())
            {
                part1Gates.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
        PrintZsInOrder();
    }

    private void PrintZsInOrder()
    {
        string s = "";
        List<LogicNode> zNodes = nodes.FindAll(item => item.Name.Substring(0, 1) == "z");
        List<LogicNode> orderedList = zNodes.OrderBy(item => Convert.ToInt32(item.Name.Substring(1, 2))).ToList();
        foreach (LogicNode zNode in orderedList)
        {
            s = Convert.ToInt32(zNode.State).ToString() + s;
        }
        Console.WriteLine(s);
        Console.WriteLine(Convert.ToInt64(s, 2));
    }

    private LogicNode FindOrMakeNode(string toFindName)
    {
        LogicNode node = nodes.Find(item => item.Name == toFindName);
        if (node == null)
        {
            node = new LogicNode(toFindName, null);
            nodes.Add(node);
        }
        return node;
    }

    private bool AllZsStated()
    {
        List<LogicNode> zNodes = nodes.FindAll(item => item.Name.Substring(0, 1) == "z");
        List<LogicNode> statedZs = zNodes.FindAll(item => item.HasState);
        return zNodes.Count == statedZs.Count;
    }
}

class LogicNode
{
    private string _name;
    private bool? _state;

    public LogicNode(string name, bool? state)
    {
        _name = name;
        _state = state;
    }

    public void AssignState(bool state)
    {
        if (_state != null)
        {
            throw new Exception();
        }
        _state = state;
    }

    public bool HasState => _state != null;
    public bool? State => _state;
    public string Name => _name;
}

class LogicGate
{
    LogicNode _input1;
    LogicNode _input2;
    LogicNode _output;
    string _operation;

    public LogicGate(LogicNode input1, LogicNode input2, LogicNode output, string operation)
    {
        _input1 = input1;
        _input2 = input2;
        _output = output;
        _operation = operation;
    }

    public bool TryOperation()
    {
        if (_input1.HasState && _input2.HasState)
        {
            switch (_operation)
            {
                case "XOR":
                    _output.AssignState((bool)_input1.State != (bool)_input2.State);
                    break;
                case "OR":
                    _output.AssignState((bool)_input1.State || (bool)_input2.State);
                    break;
                case "AND":
                    _output.AssignState((bool)_input1.State && (bool)_input2.State);
                    break;
                default:
                    throw new Exception("input was not properly parsed");
            }
            return true;
        }
        return false;
    }

    public string Operation => _operation;
    public LogicNode Input1 => _input1;
    public LogicNode Input2 => _input2;
    public LogicNode Output { get { return _output; } set { _output = value; } }
}
