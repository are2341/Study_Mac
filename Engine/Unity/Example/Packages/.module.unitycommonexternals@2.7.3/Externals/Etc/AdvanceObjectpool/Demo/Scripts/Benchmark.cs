using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// FIXME: dante (AOP 네임 스페이스 추가) {
namespace AOP {
	public class Benchmark : MonoBehaviour
	{
		[SerializeField]
		Text _spawn, _despawn;
		[SerializeField]
		InputField _input;

		ObjectPool _pool;

		[SerializeField]
		GameObject go;

		private void Start()
		{
			_pool = new ObjectPool(go);
		}

		public void OnBenchmarkButtonClick()
		{
			int i = int.Parse(_input.text);
			Stopwatch sw = new Stopwatch();
			sw.Start();
			_pool.Allocate(i);
			sw.Stop();
			_spawn.text = "Allocated " + i + " objects in: " + sw.Elapsed.TotalSeconds + " Seconds, (" + sw.Elapsed.TotalMilliseconds + " ms).";
			sw.Reset();
			sw.Start();
			_pool.DeAllocate(i);
			sw.Stop();
			_despawn.text = "Deallocated " + i + " objects in: " + sw.Elapsed.TotalSeconds + " Seconds, (" + sw.Elapsed.TotalMilliseconds + " ms).";


		}
	}
}
// FIXME: dante (AOP 네임 스페이스 추가) }
