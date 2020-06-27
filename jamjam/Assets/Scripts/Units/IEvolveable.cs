using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvolveable
{
    /// <summary>
    /// Can I evolve?
    /// </summary>
    bool evolveCondition();

    /// <summary>
    /// What happens when I evolve?
    /// </summary>
    void evolve();
}
