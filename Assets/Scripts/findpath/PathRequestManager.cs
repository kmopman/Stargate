using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathRequestManager : MonoBehaviour {

    //Generic
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    static PathRequestManager instance;
    //Generic

    //Scripts
    Pathfinding pathfinding;
    PathRequest currentPathRequest;
    //Scripts
	

    //Bool
    private bool _ProcessingPath;
    //Bool
	

	void Awake() {
		instance = this;
		pathfinding = GetComponent<Pathfinding>();
	}

	public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback) {
		PathRequest newRequest = new PathRequest(pathStart,pathEnd,callback);
		instance.pathRequestQueue.Enqueue(newRequest);
		instance.TryProcessNext();
	}

	void TryProcessNext() {
		if (!_ProcessingPath && pathRequestQueue.Count > 0) {
			currentPathRequest = pathRequestQueue.Dequeue();
			_ProcessingPath = true;
			pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
		}
	}

	public void FinishedProcessingPath(Vector3[] path, bool success) {
		currentPathRequest.callback(path,success);
		_ProcessingPath = false;
		TryProcessNext();
	}

	struct PathRequest {
		public Vector3 pathStart;
		public Vector3 pathEnd;
		public Action<Vector3[], bool> callback;

		public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback) {
			pathStart = _start;
			pathEnd = _end;
			callback = _callback;
		}

	}
}
