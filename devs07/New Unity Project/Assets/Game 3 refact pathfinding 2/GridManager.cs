using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour 
{
    public Transform NodePrefab;
    public float NodeSpacing;
    
    Vector2 NodeNumber;
    Vector2 PositionOffset;
    Node[] NodeList;
    int TempNodeCount;
    int layerMask;
    Vector3 TempRayPos;
    RaycastHit HitData;
    Vector3 TempNodePosition;

	// Use this for initialization
	void Start ()
    {
        //zera?  int TempNodeCount; 

        NodeNumber.x = Mathf.FloorToInt(transform.localScale.x / NodeSpacing);
        NodeNumber.y = Mathf.FloorToInt(transform.localScale.z / NodeSpacing);

        PositionOffset.x = transform.position.x - transform.localScale.x * 0.5f;
        PositionOffset.y = transform.position.z - transform.localScale.z * 0.5f;

        TempNodeCount = 0;//?
        for (int GridX = 0; GridX < NodeNumber.x; GridX++)
        {
            for (int GridY = 0; GridY < NodeNumber.y; GridY++)
            {
                TempNodePosition.x = PositionOffset.x + (GridX * NodeSpacing);
                TempNodePosition.z = PositionOffset.y + (GridY * NodeSpacing);
                //TempNodePosition.y = 2;

                layerMask = 1 << 8;
                TempRayPos = new Vector3(TempNodePosition.x, transform.position.y + transform.localScale.y * 0.5f, TempNodePosition.z);
                if (Physics.Raycast(TempRayPos, Vector3.down,out HitData, transform.localScale.y, layerMask))
                {
                    TempNodePosition.y = HitData.point.y;
                }

                Transform TempNodeTransform = Instantiate(NodePrefab, TempNodePosition, Quaternion.identity) as Transform;
                Node TempNode = TempNodeTransform.gameObject.GetComponent<Node>();
                TempNode.MyNodeID = TempNodeCount;

                TempNodeCount++;
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
