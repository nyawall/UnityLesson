using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 20.0f;
    [SerializeField] private LayerMask _targetLayer;
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.fixedDeltaTime);
    }
    /// <summary>
    /// �� monobehavior �� ������Ʈ�� ������ ���ӿ�����Ʈ�� �������ִ�  collider��
    /// �ٸ� collider ������Ʈ�� Rigidbody �� ������ �ִ� gameobject �� ������ ��
    /// �� �ٸ� collider �� istrigger �ɼ��� true �� �� �ش� colldier �� ���ڷ� �Ѱܹ޴� �̺�Ʈ�Լ�.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //gameobject �� layer ������Ƽ�� flag ���� �ƴԿ� ����!
        //�׷��� ����ü targetlayer �� value �� ���Ϸ���
        // flag ������ �ٲ��ֱ� ���ؼ� bit-shift ������ �ؾ���!

        if ((1<<other.gameObject.layer & _targetLayer)> 0)
        {
            // out Ű����
            // �ΰ� �̻��� �Լ� ��ȯ���� �ʿ��� �� ����ϴ� Ű����
            // out ���ڷ� �Ѱ��� ������ �ش��Լ� ��ȯ�� ���������� ���ԵǾ��� ���� ��ȯ��.
            if(other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Hurt(10.0f);
            }
            //other.gameObject.GetComponent<Enemy>().Hurt(10.0f);
            Destroy(this.gameObject);
        }
        // �����ϰ� �׽�Ʈ �غ� �� ���ڿ��� ���̾� �˻��ؼ� ����� �� ����
        //if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        //{ 
        //}
        
        
    }
    /// <summary>
    /// �浹 ����� rigidbody �� ������ �־�� ȣ���.
    /// �� ����� collider�� istrigger �ɼ��� false �̸� ȣ���.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log(collision.gameObject.name);
    }
}
