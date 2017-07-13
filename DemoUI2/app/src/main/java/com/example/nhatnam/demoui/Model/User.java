package com.example.nhatnam.demoui.Model;

import com.google.gson.annotations.SerializedName;

/**
 * Created by NHAT NAM on 6/18/2017.
 */

public class User {
    @SerializedName("ID")
    private int ID;
    @SerializedName("UserName")
    private String UserName;
//    @SerializedName("Password")
//    private String Password;
    @SerializedName("IsShipper")
    private boolean IsShipper;

    public User(int ID, String userName) {
        this.ID = ID;
        UserName = userName;
    }

    public int getID() {
        return ID;
    }


    public String getUserName() {
        return UserName;
    }

    public boolean isShipper() {
        return IsShipper;
    }

//    public String getPassword() {
//        return Password;
//    }
//
//    public void setPassword(String password) {
//        Password = password;
//    }

}
