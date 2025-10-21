using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataSetManager : MonoBehaviour
{
    ///// <summary>
    ///// 
    ///// </summary>

    //// a class for easily storing information about menu tabs as they are created
    //public class SetElement : MonoBehaviour
    //{
    //    [SerializeField]
    //    private RectTransform m_RectTransform;
    //    [SerializeField]
    //    private TMP_Text m_Text;
    //    [SerializeField]
    //    private HorizontalLayoutGroup m_DropdownLayoutGroup;
    //    [SerializeField]
    //    private List<Dropdown> m_Fields;
    //    [SerializeField]
    //    private Button m_DeleteButton;
    //    [SerializeField]
    //    private Dictionary<Dropdown, int> m_FieldConfigIndex;

    //    public RectTransform rectTransform { get { return m_RectTransform; } set { m_RectTransform = value; } }
    //    public TMP_Text text { get { return m_Text; } set { m_Text = value; } }
    //    public HorizontalLayoutGroup dropdownLayoutGroup { get { return m_DropdownLayoutGroup; } set { m_DropdownLayoutGroup = value; } }
    //    public List<Dropdown> fields { get { return m_Fields; } set { m_Fields = value; } }
    //    public Button deleteButton { get { return m_DeleteButton; } set { m_DeleteButton = value; } }
    //    public Dictionary<Dropdown, int> fieldConfigIndex { get { return m_FieldConfigIndex; } set { m_FieldConfigIndex = value; } }
    //}

    ///// <summary>
    ///// the transform of the template for the menu tabs
    ///// </summary>
    //[SerializeField] private RectTransform m_SetTemplate;
    //public RectTransform setTemplate { get { return m_SetTemplate; } set { m_SetTemplate = value; } }

    ///// <summary>
    ///// the transform of the template for the dropdown menu that is added for each field
    ///// </summary>
    //[SerializeField] private RectTransform m_DropdownTemplate;
    //public RectTransform dropdownTemplate { get { return m_DropdownTemplate; } set { m_DropdownTemplate = value; } }

    ///// <summary>
    ///// a reference to the text object the template is using
    ///// </summary>
    //[SerializeField] private TextMeshProUGUI m_Text;
    //public TextMeshProUGUI text { get { return m_Text; } set { m_Text = value; } }

    ///// <summary>
    ///// a reference to the HorizontalLayoutGroup object the template is using
    ///// </summary>
    //[SerializeField] private HorizontalLayoutGroup m_TemplateLayoutGroup;
    //public HorizontalLayoutGroup templateLayoutGroup { get { return m_TemplateLayoutGroup; } set { m_TemplateLayoutGroup = value; } }

    ///// <summary>
    ///// a reference to the Button object the template is using
    ///// </summary>
    //[SerializeField] private Button m_TemplateButton;
    //public Button templateButton { get { return m_TemplateButton; } set { m_TemplateButton = value; } }

    ///// <summary>
    ///// a reference to the Toggle object the template is using
    ///// </summary>
    //[SerializeField] private Toggle m_TemplateToggle;
    //public Toggle templateToggle { get { return m_TemplateToggle; } set { m_TemplateToggle = value; } }

    //[Space]

    ///// <summary>
    ///// the list containing all of different menus you want to be able to select
    ///// </summary>
    //private List<SetElement> m_DebrisSets;
    //public List<SetElement> debrisSets { get { return m_DebrisSets; } set { m_DebrisSets = value; } }


    //private bool validTemplate = false;
    //// makes sure the template is a valid template
    //private void SetupTemplate()
    //{
    //    validTemplate = false;

    //    if (!m_SetTemplate)
    //    {
    //        Debug.LogError("The MenuTab template is not assigned. The template needs to be assigned and must have a child GameObject with a Button component serving as the item.", this);
    //        return;
    //    }

    //    GameObject templateGo = m_SetTemplate.gameObject;
    //    templateGo.SetActive(true);
    //    Button itemButton = m_SetTemplate.GetComponent<Button>();
    //    HorizontalLayoutGroup layoutGroup = m_SetTemplate.GetComponent<HorizontalLayoutGroup>();

    //    validTemplate = true;
    //    if (!itemButton)
    //    {
    //        validTemplate = false;
    //        Debug.LogError("The SetElement template is not valid. The template must have a Button component.", setTemplate);
    //    }
    //    else if (!itemButton)
    //    {
    //        validTemplate = false;
    //        Debug.LogError("The SetElement template is not valid. The template must have a Horizontal Layout Group component.", setTemplate);
    //    }
    //    else if (!(itemButton.transform.parent is RectTransform))
    //    {
    //        validTemplate = false;
    //        Debug.LogError("The SetElement template is not valid. The template GameObject must have a RectTransform on its parent.", setTemplate);
    //    }
    //    else if (text != null && !text.transform.IsChildOf(templateGo.transform))
    //    {
    //        validTemplate = false;
    //        Debug.LogError("The SetElement template is not valid. The Item Text must be on the template GameObject or children of it.", setTemplate);
    //    }


    //    if (!validTemplate)
    //    {
    //        templateGo.SetActive(false);
    //        return;
    //    }

    //    SetElement item = templateGo.AddComponent<SetElement>();
    //    item.rectTransform = (RectTransform)templateGo.transform;
    //    item.dropdownLayoutGroup = layoutGroup;
    //    item.text = m_Text;
    //    item.deleteButton = itemButton;

    //    item.fields = new List<Dropdown>();


    //    validTemplate = true;
    //}

    ///// <summary>
    ///// loops through each of the menu data objects
    ///// and creates each of the menu tabs
    ///// then positions them properly
    ///// </summary>

    //private void Start()
    //{
    //    if (!validTemplate)
    //    {
    //        SetupTemplate();
    //        if (!validTemplate) return;
    //    }

    //    SetElement itemTemplate = m_SetTemplate.GetComponent<SetElement>();

    //    for (int i = 0; i < m_Menus.Count; i++)
    //    {
    //        MenuData menuData = m_Menus[i];
    //        MenuTab tab = createTab(menuData, i == 0, itemTemplate);

    //        if (tab == null)
    //            continue;

    //        m_Items.Add(tab);
    //    }

    //    m_Template.gameObject.SetActive(false);
    //    itemTemplate.gameObject.SetActive(false);
    //}

    ///// <summary>
    ///// initiates a single tab menu
    ///// properly setting each of its components values
    ///// then adds a listener to its button component
    ///// </summary>

    //private MenuTab createTab(MenuData data, bool selected, MenuTab template)
    //{
    //    MenuTab tab = Instantiate(template);
    //    tab.rectTransform.SetParent(template.rectTransform.parent, false);

    //    tab.gameObject.SetActive(true);

    //    if (data.text != "") tab.text.text = data.text;
    //    else tab.text.gameObject.SetActive(false);

    //    if (data.sprite != null) tab.image.sprite = data.sprite;
    //    else tab.image.gameObject.SetActive(false);

    //    tab.menuRectTransform = data.menuRectTransform;

    //    if (selected) { m_SelectedTab = tab; }
    //    tab.menuRectTransform.gameObject.SetActive(selected);

    //    tab.button.onClick.AddListener(() => OnTabSelected(tab));

    //    return tab;
    //}

    ///// <summary>
    ///// Deactivates the currently active tab
    ///// then activates the newly selected tab
    ///// </summary>
    //private void OnTabSelected(MenuTab tab)
    //{
    //    if (m_SelectedTab == tab) return;

    //    m_SelectedTab.menuRectTransform.gameObject.SetActive(false);
    //    tab.menuRectTransform.gameObject.SetActive(true);
    //    m_SelectedTab = tab;
    //}
}

