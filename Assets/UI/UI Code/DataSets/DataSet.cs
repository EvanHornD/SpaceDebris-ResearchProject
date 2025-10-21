using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataSet : MonoBehaviour
{
    #region Required Components
    [SerializeField]
    TMP_Text header;

    [SerializeField]
    HorizontalLayoutGroup horizontalLayoutGroup;

    [SerializeField]
    Button DeleteButton;

    [SerializeField]
    Toggle toggle;
    #endregion

    [Space]

    #region Prefabs
    [SerializeField]
    Parameter parameterPrefab;
    #endregion


}
