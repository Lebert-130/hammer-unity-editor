using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class mapExporter : MonoBehaviour {

    string m_Path;

    public GameObject[] blocks;

    public GameObject[] full_blocks;
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
        {
            m_Path = Application.dataPath + "/test.map";

            if (File.Exists(m_Path))
            {
                File.Delete(m_Path);
            }

            //Begin file creation
            using (FileStream fs = File.Create(m_Path))
            {
                Setup(fs);
                RegisterBlocks(fs);
                AddText(fs, "}");
            }
        }
	}

    void Setup(FileStream fs)
    {
        AddText(fs, "{\n");
        AddText(fs, "\"classname\" \"worldspawn\"\n");
        AddText(fs, "\"classname\" \"worldspawn\"\n");
        AddText(fs, "\"sounds\" \"1\"\n");
        AddText(fs, "\"MaxRange\" \"4096\"\n");
        AddText(fs, "\"mapversion\" \"220\"\n");
        AddText(fs, "\"wad\" \"\\program files (x86)\\steam\\steamapps\\common\\half-life\\valve\\halflife.wad\"\n");
    }

    void RegisterBlocks(FileStream fs)
    {
        blocks = GameObject.FindGameObjectsWithTag("Plane");
        full_blocks = GameObject.FindGameObjectsWithTag("Block");
        int sides = 0;
        int block_number = 0;
        int total_blocks = 0;

        foreach(GameObject full_block in full_blocks)
        {
            total_blocks += 1;
        }

        AddText(fs, "{\n");

        foreach(GameObject block in blocks)
        {
            var vertex1 = "(" + block.GetComponent<meshGenerator>().vertex1.x.ToString() + " " + block.GetComponent<meshGenerator>().vertex1.z.ToString() + " " + block.GetComponent<meshGenerator>().vertex1.y.ToString() + ")";
            var vertex2 = "(" + block.GetComponent<meshGenerator>().vertex2.x.ToString() + " " + block.GetComponent<meshGenerator>().vertex2.z.ToString() + " " + block.GetComponent<meshGenerator>().vertex2.y.ToString() + ")";
            var vertex3 = "(" + block.GetComponent<meshGenerator>().vertex3.x.ToString() + " " + block.GetComponent<meshGenerator>().vertex3.z.ToString() + " " + block.GetComponent<meshGenerator>().vertex3.y.ToString() + ")";

            if (block.gameObject.name == "right" || block.gameObject.name == "left")
            {
                vertex3 += " FIFTIES_FLR03 [ 0 1 0 0 ] [ 0 0 -1 0 ] 0 1 1\n";
            }
            else if (block.gameObject.name == "back" || block.gameObject.name == "front")
            {
                vertex3 += " FIFTIES_FLR03 [ 1 0 0 0 ] [ 0 0 -1 0 ] 0 1 1\n";
            }
            else if (block.gameObject.name == "top" || block.gameObject.name == "bottom")
            {
                vertex3 += " FIFTIES_FLR03 [ 1 0 0 0 ] [ 0 -1 0 0 ] 0 1 1\n";
            }

            AddText(fs, vertex1);
            AddText(fs, vertex2);
            AddText(fs, vertex3);

            sides += 1;

            if (sides == 6)
            {
                block_number += 1;
            }

            if (sides == 6 && block_number < total_blocks)
            {
                AddText(fs, "}\n");
                AddText(fs, "{\n");
                sides = 0;
            }

            if (sides == 6 && block_number >= total_blocks)
            {
                AddText(fs, "}\n");
            }
        }
    }

    private static void AddText(FileStream fs, string value)
    {
        byte[] info = Encoding.ASCII.GetBytes(value);
        fs.Write(info, 0, info.Length);
    }

}