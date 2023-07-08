using UnityEngine;
using System.Linq;

public class Helper : MonoBehaviour
{
    public static Vector3 BezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
		return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);
	}
    public static Vector3 BezierCurve2(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
		t = Mathf.Clamp01(t);
		float oneMinusT = 1f - t;
		return oneMinusT * oneMinusT * p0 + 2f * oneMinusT * t * p1 + t * t * p2;
	}
	public static RaycastHit Raycast(Vector3 origin, Vector3 direction, float maxDistance)
    {
        RaycastHit hitInfo;
        int layerMask = ~LayerMask.GetMask("Ignore Raycast");
        UnityEngine.Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask);
        return hitInfo;
    }
	public static bool CheckBox(BoxCollider box)
    {
        if (box.center != Vector3.zero) throw new System.Exception("Duno");
        if (box.size != new Vector3(1,1,1)) throw new System.Exception("Duno");

		var center = box.transform.position;
		var halfExtents = box.transform.localScale / 2;
		var rotation = box.transform.rotation;
        var layer = ~0;

        box.enabled = false;
        UnityEngine.Physics.SyncTransforms();
        var result = UnityEngine.Physics.CheckBox(center,halfExtents,rotation,layer);
        box.enabled = true;

        return result;
    }
    public static bool IsIntersects2(BoxCollider boxCollider)
    {
        var scene = UnityEngine.Physics.defaultPhysicsScene;

		var scale_x = boxCollider.size.x * boxCollider.transform.lossyScale.x;
		var scale_y = boxCollider.size.y * boxCollider.transform.lossyScale.y;
		var scale_z = boxCollider.size.z * boxCollider.transform.lossyScale.z;

		var scale = new Vector3(scale_x,scale_y,scale_z);
		
		var center = boxCollider.transform.position;
		var halfExtents = scale / 2;
		var result = new Collider[10];
		var rotation = boxCollider.transform.rotation;
		var layer = ~LayerMask.GetMask("CharacterCollider");
        var amount = scene.OverlapBox(center,halfExtents,result,rotation,layer);


        //var filtered = new List<Collider>();
        //var self = boxCollider as Collider;
        //foreach (var item in result)
        //{
        //    if (item == boxCollider) continue;
        //    filtered.Add(item);
        //}

        if (amount > 1) return true;
        return false;
    }
	public static bool IsIntersects3(BoxCollider boxCollider)
    {
        var scene = UnityEngine.Physics.defaultPhysicsScene;

		var scale_x = boxCollider.size.x * boxCollider.transform.lossyScale.x;
		var scale_y = boxCollider.size.y * boxCollider.transform.lossyScale.y;
		var scale_z = boxCollider.size.z * boxCollider.transform.lossyScale.z;

		var position_x = boxCollider.center.x * boxCollider.transform.lossyScale.x;
		var position_y = boxCollider.center.y * boxCollider.transform.lossyScale.y;
		var position_z = boxCollider.center.z * boxCollider.transform.lossyScale.z;

		var scale = new Vector3(scale_x,scale_y,scale_z);
		var position = new Vector3(position_x,position_y,position_z);
		
		
		var center = boxCollider.transform.position + position;
		Debug.DrawLine(boxCollider.transform.position, center);
		var halfExtents = scale / 2;
		var result = new Collider[10];
		var rotation = boxCollider.transform.rotation;

        var amount = scene.OverlapBox(center,halfExtents,result,rotation);

        //var filtered = new List<Collider>();
        //var self = boxCollider as Collider;
        //foreach (var item in result)
        //{
        //    if (item == boxCollider) continue;
        //    filtered.Add(item);
        //}
        if (amount > 1) return true;
        return false;
    }
    public static Vector2 InputDirectionNormalised()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var vector = new Vector2(x,y);
        if (vector.magnitude > 1)
            vector.Normalize();
        return vector;
    }
    public static Vector2 InputDirection()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var vector = new Vector2(x,y);
        return vector;
    }
    public static Vector2 InputDirection2()
    {
        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");
        var vector = new Vector2(x,y);
        return vector;
    }
    public static Vector3 InputDirectionLocal(Transform transform)
    {
        var dir = (Vector3)Helper.InputDirection();
        dir = new Vector3(dir.x, 0, dir.y);
        dir = transform.rotation * dir;
        return dir;
    }
    public static void DrawVector(Vector3 vector, Vector3 origin, Color color)
    {
        Debug.DrawLine(origin, origin+vector, color);
    }
    public static void DrawVector2(Vector3 origin, Vector3 vector, Color color)
    {
        Debug.DrawLine(origin, origin+vector, color);
    }
    public static Vector3 RandomVector3Normilesed()
    {
        var x = Random.Range(-1,1f);
        var y = Random.Range(-1,1f);
        var z = Random.Range(-1,1f);
        return new Vector3(x,y,z).normalized;
    }
    public static Vector3 GetPerp(Vector3 main, Vector3 pole)
    {
        return Vector3.ProjectOnPlane(pole,main);
    }
    public static Vector3[] PointsInside(Vector3[] points, Bounds bound) => points.Where(a => bound.Contains(a)).ToArray();
    public static Vector3 ClosestPoint(Vector3[] points, Bounds bound)
    {
        var center = bound.center;
        var point1 = points[0];
        var dist1 = Vector3.Distance(point1,center);
        for (int i = 1; i < points.Length; i++)
        {
            var point2 = points[i];
            var dist2 = Vector3.Distance(point2,center);
            if (dist2 > dist1)
            {
                point1 = point2;
                dist1 = dist2;
            }
        }
        return point1;
    }
    public static Vector3 LongestPathOut(Vector3 point, Bounds bound)
    {
        if (!bound.Contains(point)) return Vector3.zero;
        var vecMax = bound.max - point;
        var vecMin = bound.min - point;
        var max = MaxDimension(vecMax);
        var min = -MaxDimension(-vecMin);
        return max.sqrMagnitude > min.sqrMagnitude ? max : min;
    }
    public static Vector3 ShortPathOut(Vector3 point, Bounds bound)
    {
        if (!bound.Contains(point)) return Vector3.zero;
        var vecMax = bound.max - point;
        var vecMin = bound.min - point;
        var max = MinDimension(vecMax);
        var min = -MinDimension(-vecMin);
        return max.sqrMagnitude > min.sqrMagnitude ? min : max;
    }
    public static Vector3 MaxDimension(Vector3 a)
    {
        var max = Mathf.Max(new float[] {a.x,a.y,a.z});
        if (max == a.x) return new Vector3(max,0,0);
        if (max == a.y) return new Vector3(0,max,0);
                        return new Vector3(0,0,max);
    }
    public static Vector3 MinDimension(Vector3 a)
    {
        var min = Mathf.Min(new float[] {a.x,a.y,a.z});
        if (min == a.x) return new Vector3(min,0,0);
        if (min == a.y) return new Vector3(0,min,0);
                        return new Vector3(0,0,min);
    }
    public static Vector3 Longest(Vector3[] vectors)
    {
        var vec1 = vectors[0];
        for (int i = 1; i < vectors.Length; i++)
        {
            var vec2 = vectors[i];
            if (vec1.sqrMagnitude > vec2.sqrMagnitude) continue;
            vec1 = vec2;
        }
        return vec1;
    }
    public static Vector3 ShortPathOut(Vector3[] pointsInside, Bounds bound)
    {
        var boundMin = bound.min;
        var boundMax = bound.max;
        float x;
        {
            var pointsInsideRange = pointsInside.Select(a => a.x).ToArray();
            var rangeStart = boundMin.x;
            var rangeEnd = boundMax.x;
            x = ShortPathOut(pointsInsideRange,rangeStart,rangeEnd);
        }
        float y;
        {
            var pointsInsideRange = pointsInside.Select(a => a.y).ToArray();
            var rangeStart = boundMin.y;
            var rangeEnd = boundMax.y;
            y = ShortPathOut(pointsInsideRange,rangeStart,rangeEnd);
        }
        float z;
        {
            var pointsInsideRange = pointsInside.Select(a => a.z).ToArray();
            var rangeStart = boundMin.z;
            var rangeEnd = boundMax.z;
            z = ShortPathOut(pointsInsideRange,rangeStart,rangeEnd);
        }
        var vec = new Vector3(x,y,z);
        return MinAbsDimension(vec);
    }
    public static float ShortPathOut(float[] pointsInsideRange, float rangeStart, float rangeEnd)
    {
        var maxPoint = Mathf.Max(pointsInsideRange);
        var minPoint = Mathf.Min(pointsInsideRange);
        var path1 = rangeEnd - minPoint;
        var path2 = rangeStart - maxPoint;
        return path1 > -path2 ? path2 : path1;
    }
    public static Vector3 MinAbsDimension(Vector3 vector)
    {
        var x = Mathf.Abs(vector.x);
        var y = Mathf.Abs(vector.y);
        var z = Mathf.Abs(vector.z);

        var min = Mathf.Min(new float[] {x,y,z});

        if (min == x) return new Vector3(vector.x,0,0);
        if (min == y) return new Vector3(0,vector.y,0);
                      return new Vector3(0,0,vector.z);
    }
    public static float MaxX(Vector3[] points) => Mathf.Max(points.Select(a => a.x).ToArray());
    public static float MinX(Vector3[] points) => Mathf.Min(points.Select(a => a.x).ToArray());
    public static Vector3 LiftPoint(Vector3 pointToLift, Vector3 otherPoint1, float maxAlloweDistance)
    {
        var amount = 0.005f;
        while (true)
        {
            pointToLift.y += amount;
            var IsDistanceOk1 = Helper.IsDistanceOk(pointToLift,otherPoint1,maxAlloweDistance); if (!IsDistanceOk1) break;
            var IsDistanceOk2 = Helper.IsDistanceOk(pointToLift,otherPoint1,maxAlloweDistance); if (!IsDistanceOk2) break;
        }
        pointToLift.y -= amount;
        return pointToLift;
    }
    public static Vector3 LiftPoint(Vector3 pointToLift, Vector3 otherPoint1, Vector3 otherPoint2, float maxAlloweDistance)
    {
        var amount = 0.005f;
        while (true)
        {
            pointToLift.y += amount;
            var IsDistanceOk1 = Helper.IsDistanceOk(pointToLift,otherPoint1,maxAlloweDistance); if (!IsDistanceOk1) break;
            var IsDistanceOk2 = Helper.IsDistanceOk(pointToLift,otherPoint2,maxAlloweDistance); if (!IsDistanceOk2) break;
        }
        pointToLift.y -= amount;
        return pointToLift;
    }
    public static Vector3 GetMiddlePoint(Vector3 start, Vector3 end)
    {
        var vec = start - end;
        var half = vec / 2;
        var middle = end + half;
        return middle;
    }
    public static bool IsDistanceOk(Vector3 point1, Vector3 point2, float maxAlloweDistanceBetweenPoints)
    {
        var distanceBetweenPoints = Vector3.Distance(point1,point2);
        return distanceBetweenPoints < maxAlloweDistanceBetweenPoints;
    }
    public static void DrawPoint(Vector3 point, Color color)
    {
        var start = point;
        var length = 0.04f;
        for (int i = 0; i < 30; i++)
        {
            var randVec = Helper.RandomVector3Normilesed().normalized;
            var end = point+randVec*length;
            //var duration = 0.2f;
            //Debug2.DrawLine(start,end,color);
        }
    }
    public static void RotateAgent(Transform agent, Vector3 target, float delta)
    {
        var direction = target - agent.position;
        direction.y = 0;
        //Debug.DrawLine(agent.position,agent.position+direction,Color.green);
        var targetRotation = Quaternion.LookRotation(direction,Vector3.up);
        agent.rotation = Quaternion.RotateTowards(agent.rotation,targetRotation,delta);
    }
    public static void RotateAgent(Transform agent, Transform target, float delta)
    {
        var direction = target.position - agent.position;
        direction.y = 0;
        //Debug.DrawLine(agent.position,agent.position+direction,Color.green);
        var targetRotation = Quaternion.LookRotation(direction,Vector3.up);
        agent.rotation = Quaternion.RotateTowards(agent.rotation,targetRotation,delta);
    }
}