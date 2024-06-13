using UnityEngine;

namespace BTVisual
{
    public class Context 
    {
        public GameObject gameObject;
        public Transform transform;
        public Animator animator;
        public Rigidbody2D rbCompo;
        public Collider2D collider;
        
        
        public static Context CreateFromGameObject(GameObject gameObject) {
            Context context = new Context();
            context.gameObject = gameObject;
            context.transform = gameObject.transform;
            context.animator = gameObject.transform.Find("Visual").GetComponent<Animator>();
            context.rbCompo = gameObject.GetComponent<Rigidbody2D>();
            context.collider = gameObject.GetComponent<Collider2D>();
            
            //더 필요한게 있다면 여기다가 적어준다.

            return context;
        }
    }
}
