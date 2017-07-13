package com.example.nhatnam.demoui.Model;

import com.google.gson.annotations.SerializedName;

/**
 * Created by NHAT NAM on 7/4/2017.
 */

public class District {
    @SerializedName("Id")
    private int Id;
    @SerializedName("Name")
    private String Name;

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }
}
