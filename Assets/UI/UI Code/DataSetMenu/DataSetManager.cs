using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataSetManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>

    // a class for easily storing information about menu tabs as they are created
    protected class SetElement : MonoBehaviour
    {
        [SerializeField]
        private RectTransform m_RectTransform;
        [SerializeField]
        private TMP_Text m_Text;
        [SerializeField]
        private HorizontalLayoutGroup m_DropdownLayoutGroup;
        [SerializeField]
        private List<Dropdown> m_Fields;
        [SerializeField]
        private Button m_DeleteButton;
        [SerializeField]
        private Dictionary<Dropdown, int> m_FieldConfigIndex;

        public RectTransform rectTransform { get { return m_RectTransform; } set { m_RectTransform = value; } }
        public TMP_Text text { get { return m_Text; } set { m_Text = value; } }
        public HorizontalLayoutGroup dropdownLayoutGroup { get { return m_DropdownLayoutGroup; } set { m_DropdownLayoutGroup = value; } }
        public List<Dropdown> fields { get { return m_Fields; } set { m_Fields = value; } }
        public Button deleteButton { get { return m_DeleteButton; } set { m_DeleteButton = value; } }
        public Dictionary<Dropdown, int> fieldConfigIndex { get { return m_FieldConfigIndex; } set { m_FieldConfigIndex = value; } }
    }

    /// <summary>
    /// the transform of the template for the menu tabs
    /// </summary>
    [SerializeField] private RectTransform m_SetTemplate;
    public RectTransform setTemplate { get { return m_SetTemplate; } set { m_SetTemplate = value; } }
    
    /// <summary>
    /// the transform of the template for the dropdown menu that is added for each field
    /// </summary>
    [SerializeField] private RectTransform m_DropdownTemplate;
    public RectTransform dropdownTemplate { get { return m_DropdownTemplate; } set { m_DropdownTemplate = value; } }

    /// <summary>
    /// a reference to the text object the template is using
    /// </summary>
    [SerializeField] private TextMeshProUGUI m_Text;
    public TextMeshProUGUI text { get { return m_Text; } set { m_Text = value; } }

    /// <summary>
    /// a reference to the HorizontalLayoutGroup object the template is using
    /// </summary>
    [SerializeField] private HorizontalLayoutGroup m_TemplateLayoutGroup;
    public HorizontalLayoutGroup templateLayoutGroup { get { return m_TemplateLayoutGroup; } set { m_TemplateLayoutGroup = value; } }

    [Space]

    /// <summary>
    /// the list containing all of different menus you want to be able to select
    /// </summary>
    [SerializeField] private List<MenuData> m_Menus;
    public List<MenuData> MenuList { get { return m_Menus; } set { m_Menus = value; } }

    private List<MenuTab> m_Items = new List<MenuTab>();
    private MenuTab m_SelectedTab;


    private bool validTemplate = false;
    // makes sure the template is a valid template
    private void SetupTemplate()
    {
        validTemplate = false;

        if (!m_Template)
        {
            Debug.LogError("The MenuTab template is not assigned. The template needs to be assigned and must have a child GameObject with a Button component serving as the item.", this);
            return;
        }

        GameObject templateGo = m_Template.gameObject;
        templateGo.SetActive(true);
        Button itemButton = m_Template.GetComponent<Button>();

        validTemplate = true;
        if (!itemButton)
        {
            validTemplate = false;
            Debug.LogError("The MenuTab template is not valid. The template must have a Button component.", template);
        }
        else if (!(itemButton.transform.parent is RectTransform))
        {
            validTemplate = false;
            Debug.LogError("The MenuTab template is not valid. The template GameObject must have a RectTransform on its parent.", template);
        }
        else if (text != null && !text.transform.IsChildOf(templateGo.transform))
        {
            validTemplate = false;
            Debug.LogError("The MenuTab template is not valid. The Item Text must be on the template GameObject or children of it.", template);
        }
        else if (image != null && !image.transform.IsChildOf(templateGo.transform))
        {
            validTemplate = false;
            Debug.LogError("The MenuTab template is not valid. The Item Image must be on the template GameObject or children of it.", template);
        }

        if (!validTemplate)
        {
            templateGo.SetActive(false);
            return;
        }

        MenuTab item = templateGo.AddComponent<MenuTab>();
        item.text = m_Text;
        item.image = m_Image;
        item.button = itemButton;
        item.rectTransform = (RectTransform)templateGo.transform;


        validTemplate = true;
    }

    /// <summary>
    /// loops through each of the menu data objects
    /// and creates each of the menu tabs
    /// then positions them properly
    /// </summary>

    private void Start()
    {
        if (!validTemplate)
        {
            SetupTemplate();
            if (!validTemplate) return;
        }

        MenuTab itemTemplate = m_Template.GetComponent<MenuTab>();

        for (int i = 0; i < m_Menus.Count; i++)
        {
            MenuData menuData = m_Menus[i];
            MenuTab tab = createTab(menuData, i == 0, itemTemplate);

            if (tab == null)
                continue;

            m_Items.Add(tab);
        }

        // reposition all menu tabs now that they have all been added

        GameObject content = itemTemplate.rectTransform.parent.gameObject;
        RectTransform contentRectTransform = content.transform as RectTransform;
        int numMenus = m_Items.Count;
        float menuFraction = (1f / numMenus);

        for (int i = 0; i < numMenus; i++)
        {
            RectTransform itemRect = m_Items[i].rectTransform;
            itemRect.anchorMin = new Vector2(i * menuFraction, 0);
            itemRect.anchorMax = new Vector2((i + 1) * menuFraction, 1);

            itemRect.anchoredPosition = Vector2.zero;

            itemRect.offsetMin = Vector2.zero;
            itemRect.offsetMax = Vector2.zero;
        }

        m_Template.gameObject.SetActive(false);
        itemTemplate.gameObject.SetActive(false);
    }

    /// <summary>
    /// initiates a single tab menu
    /// properly setting each of its components values
    /// then adds a listener to its button component
    /// </summary>

    private MenuTab createTab(MenuData data, bool selected, MenuTab template)
    {
        MenuTab tab = Instantiate(template);
        tab.rectTransform.SetParent(template.rectTransform.parent, false);

        tab.gameObject.SetActive(true);

        if (data.text != "") tab.text.text = data.text;
        else tab.text.gameObject.SetActive(false);

        if (data.sprite != null) tab.image.sprite = data.sprite;
        else tab.image.gameObject.SetActive(false);

        tab.menuRectTransform = data.menuRectTransform;

        if (selected) { m_SelectedTab = tab; }
        tab.menuRectTransform.gameObject.SetActive(selected);

        tab.button.onClick.AddListener(() => OnTabSelected(tab));

        return tab;
    }

    /// <summary>
    /// Deactivates the currently active tab
    /// then activates the newly selected tab
    /// </summary>
    private void OnTabSelected(MenuTab tab)
    {
        if (m_SelectedTab == tab) return;

        m_SelectedTab.menuRectTransform.gameObject.SetActive(false);
        tab.menuRectTransform.gameObject.SetActive(true);
        m_SelectedTab = tab;
    }
}

