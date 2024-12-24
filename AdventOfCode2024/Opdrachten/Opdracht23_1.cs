using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024;

class Opdracht23_1 : IOpdracht
{
    public void Run()
    {
        Dictionary<string, ConnectionNode> dic = new Dictionary<string, ConnectionNode>();
        StreamReader sr = new StreamReader("..\\..\\..\\Resources\\O23-1.txt");
        string line = sr.ReadLine();
        while (line != null && line != "")
        {
            string[] names = line.Split('-');
            if (!dic.ContainsKey(names[0]))
            {
                dic.Add(names[0], new ConnectionNode(names[0]));
            }
            if (!dic.ContainsKey(names[1]))
            {
                dic.Add(names[1], new ConnectionNode(names[1]));
            }
            dic[names[0]].AddConnection(dic[names[1]]);
            line = sr.ReadLine();
        }
        tNumber(dic);

        List<ConnectionNode> LargestNodeNetwork = new List<ConnectionNode>();
        LargestNodeNetwork = Part2(dic, LargestNodeNetwork);

        PrintPassword(LargestNodeNetwork);
    }

    private List<ConnectionNode> Part2(Dictionary<string, ConnectionNode> dic, List<ConnectionNode> LargestNodeNetwork)
    {
        foreach (string name in dic.Keys)
        {
            int largestlarge = LargestNodeNetwork.Count;
            ConnectionNode node = dic[name];
            List<List<ConnectionNode>> masterList = new List<List<ConnectionNode>>();
            LargestNetwork(ref masterList, new List<ConnectionNode> { node }, largestlarge);
            foreach (var item in masterList)
            {
                if (item.Count > LargestNodeNetwork.Count)
                {
                    LargestNodeNetwork = item;
                }
            }
        }

        return LargestNodeNetwork;
    }

    private void PrintPassword(List<ConnectionNode> LargestNodeNetwork)
    {
        List<ConnectionNode> SortedList = LargestNodeNetwork.OrderBy(o => o.NameNumber).ToList();
        string s = "";
        for (int i = 0; i < SortedList.Count; i++)
        {
            s += SortedList[i].Name;
            s += ',';
        }
        s = s.TrimEnd(',');
        Console.WriteLine(s);
    }

    private List<ConnectionNode> LargestNetwork(ref List<List<ConnectionNode>> masterList, List<ConnectionNode> previousNodes, int maxSize)
    {
        ConnectionNode lastNode = previousNodes.Last();
        if(lastNode.Connections.Count < maxSize || lastNode.Connections.Count < previousNodes.Count)
        {
            return previousNodes;
        }
        foreach (ConnectionNode node in lastNode.Connections)
        {
            if(node.NameNumber > lastNode.NameNumber && node.ConnectsToAll(previousNodes))
            {
                var newPreviousConnections = new List<ConnectionNode>(previousNodes);
                newPreviousConnections.Add(node);
                masterList.Add(LargestNetwork(ref masterList, newPreviousConnections, maxSize));
            }
        }
        return previousNodes;
    }

    private void tNumber(Dictionary<string, ConnectionNode> dic)
    {
        int t = 0;
        foreach (string name in dic.Keys)
        {
            ConnectionNode node = dic[name];
            t += node.Sequence();
        }
        Console.WriteLine(t);
    }
}

class ConnectionNode
{
    private string _name;
    public List<ConnectionNode> Connections;

    public ConnectionNode(string name)
    {
        _name = name;
        Connections = new List<ConnectionNode>();
    }

    public void AddConnection(ConnectionNode node)
    {
        Connections.Add(node);
        node.Connections.Add(this);
    }

    public int NameNumber
    {
        get
        {
            return 1000 * _name[0] + _name[1];
        }
    }

    public bool HasTea => _name[0] == 't';

    public int Sequence()
    {
        int ts = 0;
        foreach (ConnectionNode node in Connections)
        {
            if(node.NameNumber > NameNumber)
            {
                ts += node.Sequence(this);
            }
        }
        return ts;
    }

    private int Sequence(ConnectionNode firstNode)
    {
        int ts = 0;
        foreach(ConnectionNode node in Connections)
        {
            if(node.NameNumber > NameNumber)
            {
                ts += node.Sequence(firstNode, this);
            }
        }
        return ts;
    }

    private int Sequence(ConnectionNode firstNode, ConnectionNode secondNode)
    {
        foreach(ConnectionNode node in Connections)
        {
            if(node == firstNode)
            {
                if(firstNode.HasTea || secondNode.HasTea || HasTea)
                {
                    return 1;
                }
                return 0;  
            }
        }
        return 0;
    }

    public bool ConnectsToAll(List<ConnectionNode> nodes)
    {
        foreach (ConnectionNode node in nodes)
        {
            if(!Connections.Contains(node))
            {
                return false;
            }
        }
        return true;
    }

    public string Name => _name;
}
