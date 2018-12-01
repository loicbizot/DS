using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollingText : MonoBehaviour {

    public string message;
    private RectTransform textTransform;
    private TextMeshProUGUI text;
    public GameObject textToClone;
    
    private List<RectTransform> rectTransforms;
    private List<GameObject> clones;
    private int nbOfTexts;
    private float textSize;

    private void Awake()
    {

        textTransform = textToClone.GetComponent<RectTransform>();
        text = textToClone.GetComponent<TextMeshProUGUI>();

        rectTransforms = new List<RectTransform>();
        clones = new List<GameObject>();

        OnTextChange();

    }

    public void OnTextChange()
    {

        // destruction des anciens champs de texte
        foreach(GameObject clone in clones)
            Destroy(clone);
        clones.Clear();
        rectTransforms.Clear();

        // mise à jour du champ de texte principal
        text.text = message;
        LayoutRebuilder.ForceRebuildLayoutImmediate(textTransform);
        textSize = textTransform.sizeDelta.x;

        // rien à afficher
        if(textSize == 0)
            return;

        // création de a chaine de textes
        nbOfTexts = (int)(Display.main.renderingWidth / (textSize + text.font.tabSize)) + 2;
        for(int i = 0; i < nbOfTexts; i++)
        {
            GameObject newUI = Instantiate(textToClone, this.transform);
            RectTransform transform = newUI.GetComponent<RectTransform>();
            transform.anchoredPosition = new Vector2((textSize + text.font.tabSize) * i, 0);
            rectTransforms.Add(transform);
            clones.Add(newUI);
        }

    }

    // Update is called once per frame
    void Update () {

        // verifications taille de l'écran
        if((int)(Display.main.renderingWidth / (textSize + text.font.tabSize)) + 2 != nbOfTexts)
            OnTextChange();
        
        // movement des textes
        foreach(RectTransform transform in rectTransforms)
        {
            transform.anchoredPosition -= new Vector2(1, 0);
            if(transform.anchoredPosition.x <= -(textSize + text.font.tabSize))
            {
                transform.anchoredPosition += new Vector2((textSize + text.font.tabSize) * nbOfTexts, 0);
            }
        }

	}

}
