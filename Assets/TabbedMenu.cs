using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabbedMenu : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    
    // a class for easily storing information about menu tabs as they are created
    protected class MenuTab
    {
        [SerializeField]
        private TMP_Text m_Text;
        [SerializeField]
        private Image m_Image;
        [SerializeField]
        private RectTransform m_RectTransform;
        [SerializeField]
        private Button m_Button;

        public TMP_Text text { get { return m_Text; } set { m_Text = value; } }
        public Image image { get { return m_Image; } set { m_Image = value; } }
        public RectTransform rectTransform { get { return m_RectTransform; } set { m_RectTransform = value; } }
        public Button button { get { return m_Button; } set { m_Button = value; } }
    }
    
    /// <summary>
    /// Class for storing a single menu, and the settings for its menu tab
    /// </summary>
    [Serializable]
    public class MenuData
    {
        [SerializeField] private RectTransform m_MenuRectTransform;

        [SerializeField] private string m_TabText = "Default_Text";

        [SerializeField] private Sprite m_Sprite;

        /// <summary>
        /// the menus RectTransform
        /// </summary>
        public RectTransform menuRectTransform { get { return m_MenuRectTransform; } set { m_MenuRectTransform = value; } }

        /// <summary>
        /// The text which is displayed in this menus tab at the top of the menu
        /// </summary>
        public string tabText { get { return m_TabText; } set { m_TabText = value; } }

        /// <summary>
        /// the menus tab icon which is displayed right next to the text
        /// </summary>
         
        public Sprite sprite { get { return m_Sprite; } set { m_Sprite = value; } }
    }


    /// <summary>
    /// the transform of the template for the menu tabs
    /// </summary>
    [SerializeField] private RectTransform m_Template;
    public RectTransform template { get { return m_Template; } set { m_Template = value; } }

    /// <summary>
    /// the color of the tabs when they are selected vs not selected
    /// </summary>
    [SerializeField] private Color m_SelectedColor;
    public Color selectedColor { get { return m_SelectedColor; } set { m_SelectedColor = value; } }

    [SerializeField] private Color m_DeselectedColor;
    public Color deselectedColor { get { return m_DeselectedColor; } set { m_DeselectedColor = value; } }

    /// <summary>
    /// a reference to the text object the template is using
    /// </summary>
    [SerializeField] private Text m_Text;
    public Text text { get { return m_Text; } set { m_Text = value; } }

    /// <summary>
    /// a reference to the image object the template is using
    /// </summary>
    [SerializeField] private Image m_Image;
    public Image image { get { return m_Image; } set { m_Image = value; } }

    [Space]

    /// <summary>
    /// the list containing all of different menus you want to be able to select
    /// </summary>
    [SerializeField] private List<MenuData> m_Menus;
    public List<MenuData> MenuList { get { return m_Menus; } set { m_Menus = value; } }


    private bool validTemplate = false;
    // makes sure the template is a valid template
    private void SetupTemplate()
    {
        validTemplate = false;

        if (!m_Template)
        {
            Debug.LogError("The dropdown template is not assigned. The template needs to be assigned and must have a child GameObject with a Button component serving as the item.", this);
            return;
        }

        GameObject templateGo = m_Template.gameObject;
        templateGo.SetActive(true);
        Button itemButton = m_Template.GetComponent<Button>();

        validTemplate = true;
        if (!itemButton || itemButton.transform == template)
        {
            validTemplate = false;
            Debug.LogError("The dropdown template is not valid. The template must have a Button component.", template);
        }
        else if (!(itemButton.transform.parent is RectTransform))
        {
            validTemplate = false;
            Debug.LogError("The dropdown template is not valid. The template GameObject must have a RectTransform on its parent.", template);
        }
        else if (text != null && !text.transform.IsChildOf(itemButton.transform))
        {
            validTemplate = false;
            Debug.LogError("The dropdown template is not valid. The Item Text must be on the template GameObject or children of it.", template);
        }
        else if (image != null && !image.transform.IsChildOf(itemButton.transform))
        {
            validTemplate = false;
            Debug.LogError("The dropdown template is not valid. The Item Image must be on the template GameObject or children of it.", template);
        }

        if (!validTemplate)
        {
            templateGo.SetActive(false);
            return;
        }
    }
}
