﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewArchiveMenu : MonoBehaviour {

    public ArhiveGame archive;
    GameObject iconPrefab;
    private int view = 0, step = 0;
    private IconPlayer icon;
    private IconPlayer[] iconList;
    float startX = -7.063f;
    float startY = 3.6019f;

    // Use this for initialization
    void Start() {
        while (true) {
            if (this.GetComponent<Controller>().archive == null)
                continue;
            archive = this.GetComponent<Controller>().archive;
            iconList = new IconPlayer[6];
            break;
        }
    }

    // Update is called once per frame
    void Update() {
        if (step != archive.getNumStep()) {
            if (archive.getStepPlayer(archive.getNumStep()) == 1) {
                StartCoroutine(createIcon());
                step++;
            }
            else {
                icon = new IconPlayer(archive, archive.getNumStep(), archive.getStepPlayer(archive.getNumStep()));
                addIconList(icon);
                showIcon();
                step++;
            }
        }
    }

    public IEnumerator createIcon() {
        yield return new WaitForSeconds(2f);
        icon = new IconPlayer(archive, archive.getNumStep(), archive.getStepPlayer(archive.getNumStep()));
        addIconList(icon);
        showIcon();
    }

    public void showIcon() {
        int j = 0;
        for (int i = 5; i >= 0; i--) {
            if (view >= i) {
                icon = iconList[i];
                iconPrefab = icon.getIconPrefab();
                float x = startX;
                float y = (startY - 1.4281f * j);
                Vector3 positionIcon = new Vector3(x, y, 0.0f);
                iconPrefab.transform.position = positionIcon;
                j++;
            }
        }
        view++;
    }

    public void addIconList(IconPlayer icon) {
        if (view >= 6) {
            IconPlayer[] iconListTemp = new IconPlayer[6];
            for (int i = 5; i >= 0; i--)
                iconListTemp[i] = new IconPlayer(iconList[i]);

            for (int i = 5; i >= 0; i--) {
                switch (i) {
                    case 0:
                        iconList[i].destroyPrefab();
                        iconList[i] = null;
                        iconList[i] = iconListTemp[i + 1];
                        break;
                    case 5:
                        iconList[i] = icon;
                        break;
                    default:
                        iconList[i] = iconListTemp[i + 1];
                        break;
                }
            }
        }
        else {
            iconList[view] = icon;
        }
        updateNumbers();
    }

    void updateNumbers() {
        if(view >= 6) {
            for (int i = 0; i < 6; i++) {
                iconList[i].setNumber(5 - i);
            }
        }
        else {
            for (int i = 0; i <=view ; i++) {
                iconList[i].setNumber(view - i);
            }
        }
    }
}

