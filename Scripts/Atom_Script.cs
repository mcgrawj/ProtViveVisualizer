using UnityEngine;
using System.Collections;


/// Hydrogen = White H = 25pm
/// Oxygen = Red O = 60pm
/// Nitrogen = Blue N = 65pm
/// Carbon = Grey C = 70pm
/// Sulphur = Yellow S = 100
/// Other = Maroon M 
///       Zinc = 135pm
///       Iron = 140pm
///       Sodium = 180pm
///       Potassium = 220pm
///       Calcium = 180
///       https://en.wikipedia.org/wiki/Atomic_radii_of_the_elements_(data_page)
///       https://en.wikipedia.org/wiki/Ionic_radius
///       https://en.wikipedia.org/wiki/Atomic_radius
///       https://en.wikipedia.org/wiki/Covalent_radius



public class Atom_Script : MonoBehaviour {

    public Color color = new Color();
    public Vector3 position = new Vector3();
    public string atomName = "";
    public float scale = 0.3f;
    public bool isBackbone = true;

    public LineRenderer LR;
    private Renderer REND;

    private Vector3 BaseSize;

    void Start() {
        this.transform.position = position * scale;
        this.color = NameToColor(name);
        this.transform.localScale = this.transform.localScale * NameToSize(name);
        LR = this.gameObject.AddComponent<LineRenderer>();
        LR.enabled = false;
        REND = this.gameObject.GetComponent<Renderer>();
    }

    void Update() {
        this.gameObject.name = atomName;
        REND.material.color = color;
    }

    public static Color NameToColor(string name) {
        switch (name[0].ToString().ToUpper()) {
            case "C":
                return Color.grey;
            case "H":
                return Color.white;
            case "O":
                return Color.red;
            case "N":
                return Color.cyan;
            case "S":
                return Color.yellow;
        }

        return Color.black;
    }

    public static float NameToSize(string name) {
        switch (name[0].ToString().ToUpper()) {
            case "C":
                return 0.7f;
            case "H":
                return 0.25f;
            case "O":
                return 0.6f;
            case "N":
                return 0.65f;
            case "S":
                return 1f;
        }

        return 0.5f;
    }
}
