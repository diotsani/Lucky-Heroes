using UnityEngine;

namespace Enemy
{
    public class EnemyMotor : MonoBehaviour
    {
        [SerializeField] private EnemyBrain brain;
        
        public void MoveTo(Vector2 target, bool run)
        {
            /*if(((Vector2)transform.position - target).sqrMagnitude < 0.0001f)return;
            
            Vector2 dir = (target - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(dir * brain.GetStats().RunSpeed * Time.deltaTime);*/

            float speed = run ? brain.GetStats().RunSpeed : brain.GetStats().WalkSpeed;

            Vector2 current = transform.position;
            Vector2 next = Vector2.MoveTowards(current, target, speed * Time.deltaTime);
            Vector2 dir = next - current;
            
            transform.position = next;
            
            if(!SuccessMove(dir))return;
            {
                Rotate(dir);
            }
        }

        private void Rotate(Vector2 direction)
        {
            if(direction.x == 0)return;
            transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
        }
        
        public void Stop()
        {
            
        }

        public bool SuccessMove(Vector2 dir)
        {
            return dir.sqrMagnitude < 0.0001f;
        }
    }
}