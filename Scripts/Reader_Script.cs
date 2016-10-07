using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Reader_Script : MonoBehaviour {

    public static List<Atom_Script> Atoms_Backbones = new List<Atom_Script>();
    public static List<Atom_Script> Atoms_All = new List<Atom_Script>();
    public float scale = 0.3f;

    [SerializeField]
    Material LineMat;
    bool isBackboneOnly = true;

    bool Init = false;

    void Start() {

        TextAsset pdb_file = Resources.Load<TextAsset>("3tn2");
        if (pdb_file == null) {
            Debug.Log("PDB is null");
            return;
        }

        string pdb = pdb_file.text;
        string[] pdb_array = pdb.Split('\n');

        int index = 0;
        foreach (string line in pdb_array) {
            if (line.IndexOf("ATOM") == 0) {

                Atom_Script myAtom;

                index++;
                string[] parameters = line.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);


                GameObject atomMol = GameObject.CreatePrimitive(PrimitiveType.Sphere); //Create a primitive (already equipt with the sphere component) where we can attach our Residue_Script script

                myAtom = atomMol.AddComponent<Atom_Script>();
                myAtom.position.x = float.Parse(parameters[6]);
                myAtom.position.y = float.Parse(parameters[7]);
                myAtom.position.z = float.Parse(parameters[8]);

                myAtom.atomName = atomMol.name = parameters[2];

                if (myAtom.atomName.ToUpper() == "C") {
                    Residue_Script res_script = new Residue_Script();
                    res_script.Atoms.Add(myAtom);
                    myAtom.isBackbone = true;
                    Atoms_Backbones.Add(myAtom);
                }
                Atoms_All.Add(myAtom);
            }
        }
        Debug.Log("PDB COMPLETE");
    }


    void Update() {
        if (!Init) {
            Init = true;

            for (int i = 1; i < Atoms_Backbones.Count; i++) {
                ConnectAtoms(Atoms_Backbones[i], Atoms_Backbones[i - 1]);

            }
            if (isBackboneOnly)
                for (int i = 0; i < Atoms_All.Count; i++) {
                    Atoms_All[i].gameObject.SetActive(Atoms_All[i].isBackbone);
                }
        }
    }

    /// <summary>
    /// Adds a line between two atoms.
    /// </summary>
    /// <param name="A0">Atom 1</param>
    /// <param name="A1">Atom 2</param>
    void ConnectAtoms(Atom_Script A0, Atom_Script A1) {
        LineRenderer LR = A0.LR; //Grab the line renderer on the Atom
        LR.enabled = true;

        LR.SetPosition(0, new Vector3( //Set start
            A0.position.x,
            A0.position.y,
            A0.position.z));

        LR.SetPosition(1, new Vector3( //Set end
            A1.position.x,
            A1.position.y,
            A1.position.z));

        LR.SetWidth(.3f, .3f);
        LR.material = LineMat; //Assign a white material

        LR.SetColors(A0.color, A1.color);
    }

}
