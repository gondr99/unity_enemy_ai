using System;
using System.Collections.Generic;

namespace Gondr.Astar
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        public List<T> _heap = new List<T>();
        public int Count
        {
            get { return _heap.Count; }
        }

        public T Contains(T t)
        {
            int idx = _heap.IndexOf(t);
            if (idx < 0) return default(T);
            return _heap[idx];
        }

        public void Push(T data)
        {
            //힙의 맨 끝에 새로운 데이터를 삽입한다.
            _heap.Add(data);
            int now = _heap.Count - 1;
            while (now > 0)
            {
                //자기보다 우선순위가 밀리는 것이 있는지 찾는다.
                int next = (now - 1) / 2;
                if (_heap[now].CompareTo(_heap[next]) < 0)
                {
                    break;
                }
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                now = next;
            }
        } //힙의 맨위에 우선순위가 가장 빠른 녀석이 들어와있다.

        public T Pop()
        {
            T ret = _heap[0];

            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex); //마지막 원소를 맨 위로 끌어오고 마지막은 삭제
            lastIndex--;

            //이제 내려가면서 자기 자리 찾아가기 (push의 반대)
            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;
                //왼쪽값이 지금 값보다 크다면 왼쪽으로 내려가고 왼쪽거를 내자리로 올림
                if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                {
                    next = left;
                }

                //오른쪽이 그렇다면 (만약 위에서 왼쪽이 타겟이 되었어도 이경우는 오른쪽으로 타겟이 변경된다.
                if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                {
                    next = right;
                }

                //만약 오른쪽이나 왼쪽이 나보다 다 작다는 말이 되니까 이러면 걍 종료
                if (next == now)
                    break;

                //그렇지 않다면 교체해서 내려가야함
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                now = next;
            }
            return ret;
        }

        public T Peek()
        {
            //T타입의 기본값을 리턴하고 그렇지 않다면 0번째를 리턴하고
            return _heap.Count == 0 ? default(T) : _heap[0];
        }
    }
}

