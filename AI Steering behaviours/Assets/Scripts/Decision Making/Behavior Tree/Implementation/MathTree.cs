using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class MathTree : MonoBehaviour
{
    // Tree Nodes
    private Selector rootNode;

    private ActionNode node2A;
    private Inverter node2B;
    private ActionNode node2C;
    private ActionNode node3;

    // Debug
    private Color evaluating = Color.yellow;
    private Color succeeded = Color.green;
    private Color failed = Color.red;

    public Buttons rootNodeButton;
    public Buttons node2AButton;
    public Buttons node2BButton;
    public Buttons node2CButton;
    public Buttons node3Button;

    // Variables
    public int targetValue = 20;
    private int _currentValue = 0;

    [SerializeField]
    private Text valueLabel;

    public static Thread t;

    // We instantiate our nodes from the bottom up because 
    // we cannot instantiate a parent without passing its child nodes
    void Start () {

        rootNode = new Selector(
            node2A = new ActionNode(AddTen),
            node2B = new Inverter(
                node3 = new ActionNode(NotEqualToTarget)),
            node2C = new ActionNode(AddTen)
            );

        #region other way
        //// Node3 has no children
        //node3 = new ActionNode(NotEqualToTarget);

        //node2A = new ActionNode(AddTen);
        //node2C = new ActionNode(AddTen);

        //// Node 2B is an inverter which has node 3 as a child so we pass node3 in
        //node2B = new Inverter(node3);

        //// Lastly, our root node and we need to pass the list of children
        //List<BNode> rootChildren = new List<BNode>();
        //rootChildren.Add(node2A);
        //rootChildren.Add(node2B);
        //rootChildren.Add(node2C);

        //rootNode = new Selector(node2A, node2B, node2C);
        #endregion

        // Debug
        valueLabel.text = _currentValue.ToString();

        UpdateUIButtons();
        rootNode.Run();

        t = new Thread(new ThreadStart(threadStarter));
        t.IsBackground = true;
        t.Start();

        UpdateUI();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) t.Join();
    }

    void threadStarter()
    {
        while (true)
        {
            try
            {
                rootNode.Run();
            }
            catch (ThreadAbortException)
            {
                // Assuming we wanted this, make the tree stop walking, regardless of where it is.
                break;
            }
        }
    }

    private BNodeStatus NotEqualToTarget()
    {
        if (_currentValue != targetValue)   return BNodeStatus.Success;
        else                                return BNodeStatus.Failure;
    }

    private BNodeStatus AddTen()
    {
        _currentValue += 10;
        valueLabel.text = _currentValue.ToString();

        if (_currentValue == targetValue)   return BNodeStatus.Success;
        else                                return BNodeStatus.Failure;
    }

    private void UpdateUIButtons()
    {
        rootNode.Button = rootNodeButton;

        node2A.Button = node2AButton;

        node2B.Button = node2BButton;

        node2C.Button = node2CButton;

        node3.Button = node3Button;
    }

    private void UpdateUI()
    {
        if (rootNode.NodeStatus == BNodeStatus.Success) rootNodeButton.SetColor(succeeded);
        if (rootNode.NodeStatus == BNodeStatus.Failure) rootNodeButton.SetColor(failed);

        if (node2A.NodeStatus == BNodeStatus.Success) node2AButton.SetColor(succeeded);
        if (node2A.NodeStatus == BNodeStatus.Failure) node2AButton.SetColor(failed);

        if (node2B.NodeStatus == BNodeStatus.Success) node2BButton.SetColor(succeeded);
        if (node2B.NodeStatus == BNodeStatus.Failure) node2BButton.SetColor(failed);

        if (node2C.NodeStatus == BNodeStatus.Success) node2CButton.SetColor(succeeded);
        if (node2C.NodeStatus == BNodeStatus.Failure) node2CButton.SetColor(failed);

        if (node3.NodeStatus == BNodeStatus.Success) node3Button.SetColor(succeeded);
        if (node3.NodeStatus == BNodeStatus.Failure) node3Button.SetColor(failed);
    }
}
