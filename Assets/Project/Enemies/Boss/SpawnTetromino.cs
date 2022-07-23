using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    private int _nextTetrominoes;
    public GameObject[] Tetrominoes;
    private GameObject _heldTetrominoes;

    // Start is called before the first frame update
    void Start()
    {
        _nextTetrominoes = Random.Range(0, Tetrominoes.Length);
        NewTetromino();
    }

    public void NewTetromino()
    {
        if (_heldTetrominoes!=null)
        {
            Destroy(_heldTetrominoes);
        }
        Instantiate(Tetrominoes[_nextTetrominoes], transform.position, Quaternion.identity);
        _nextTetrominoes = Random.Range(0, Tetrominoes.Length);
        _heldTetrominoes = Instantiate(Tetrominoes[_nextTetrominoes], transform.position, Quaternion.identity);
        _heldTetrominoes.GetComponent<TetrisBlock>().enabled = false;
        _heldTetrominoes.GetComponent<TetrisBlockInteraction>().enabled = false;
        _heldTetrominoes.transform.SetParent(transform);
    }
}