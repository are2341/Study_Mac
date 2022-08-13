using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: dante (AOP 네임 스페이스 추가) {
namespace AOP {
	public class ScriptableobjectObjectPoolTest : MonoBehaviour
	{
		[SerializeField]
		ScriptableObjectObjectPool _objectPool;
		[SerializeField]
		int numberstospawn = 1000;

		// Start is called before the first frame update
		void Start()
		{
			_objectPool.Init();
		}

		// Update is called once per frame
		void Update()
		{
			if (_objectPool["Cubes"].Count < numberstospawn)
			{
				_objectPool["Cubes"].Spawn(this.transform.position);
			}
			if (_objectPool["Spheres"].Count < numberstospawn)
			{
				_objectPool["Spheres"].Spawn(this.transform.position);
			}
		}
	}
}
// FIXME: dante (AOP 네임 스페이스 추가) }
