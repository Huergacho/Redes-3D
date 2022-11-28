
using System;
using Photon.Pun;


public class PoinstUI : MonoBehaviourPun
{
    public event Action<int> OnAddingPoint;
    
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
