using UnityEngine;
public class UserControl : MonoBehaviour
{
	[SerializeField] private Camera gameCamera;
	public GameObject Unit;
	private void Awake()
	{
		Unit = GameObject.Find("Player");
	}
	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			var ray = gameCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				Unit.GetComponent<Unit>().SetDestination(hit.point);
			}
		}
	}
}