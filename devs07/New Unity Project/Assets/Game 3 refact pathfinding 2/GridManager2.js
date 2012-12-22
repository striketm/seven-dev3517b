public var NodePrefab : Transform;
public var NodeSpacing : float;
var NodeNumber : Vector2;
var PositionOffset : Vector2;
var NodeList : Node2[];


function Start () {
		NodeNumber.x = Mathf.FloorToInt(transform.localScale.x / NodeSpacing);
		NodeNumber.y = Mathf.FloorToInt(transform.localScale.z / NodeSpacing);
	
		PositionOffset.x = transform.position.x - transform.localScale.x*0.5;
		PositionOffset.y = transform.position.z - transform.localScale.z*0.5;
	
		var TempNodeCount : int;	
		for(var GridX : int = 0; GridX < NodeNumber.x; GridX ++){
			for(var GridY : int = 0; GridY < NodeNumber.y; GridY ++){
		
				var TempNodePosition : Vector3;
				TempNodePosition.x = PositionOffset.x + (GridX * NodeSpacing);
				TempNodePosition.z = PositionOffset.y + (GridY * NodeSpacing);
				
                var HitData : RaycastHit;
			var layerMask = 1 << 8;
			var TempRayPos : Vector3;
			TempRayPos = Vector3(TempNodePosition.x,transform.position.y+transform.localScale.y*0.5,TempNodePosition.z);
			if (Physics.Raycast(TempRayPos, -Vector3.up, HitData, transform.localScale.y,layerMask)) {
				TempNodePosition.y = HitData.point.y;
			}

		
				var TempNodeTransform : Transform = Instantiate(NodePrefab, TempNodePosition,Quaternion.identity);
				var TempNode : Node2 = TempNodeTransform.gameObject.GetComponent(Node2);
				TempNode.MyNodeID = TempNodeCount;
			
				TempNodeCount ++;
			}
		}
}


