package com.example.nhatnam.demoui.Presenter.Activity;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.util.Log;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Toast;

import com.example.nhatnam.demoui.Model.API.UserAPI;
import com.example.nhatnam.demoui.Model.API.Utils.RetrofitUtils;
import com.example.nhatnam.demoui.Model.User;
import com.example.nhatnam.demoui.R;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by NHAT NAM on 6/18/2017.
 */

public class LoginActivity extends Activity implements View.OnClickListener {
    public static User user;
    private ProgressDialog myProgress;
    private String username, password;
    public static final String PREFS_NAME = "MyPrefsFile";
    private static final String PREF_USERNAME = "username";
    private static final String PREF_PASSWORD = "password";
    private Button btnLogin;
    private EditText edtID;
    private EditText edtPassword;
    private CheckBox isRemember;
    private SharedPreferences loginPreferences;
    private SharedPreferences.Editor loginPrefsEditor;
    private Boolean saveLogin;
    private UserAPI mUserAPI;

    private interface Listener {
        void onResult(User user);
    }

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.login);
        btnLogin = (Button) findViewById(R.id.btnLogin);
        edtID = (EditText) findViewById(R.id.userid);
        edtPassword = (EditText) findViewById(R.id.user_password);
        isRemember = (CheckBox) findViewById(R.id.isRemember);
        loginPreferences = getSharedPreferences("loginPrefs", MODE_PRIVATE);
        loginPrefsEditor = loginPreferences.edit();
        saveLogin = loginPreferences.getBoolean("saveLogin", false);
        if (saveLogin == true) {
            showProgressBar();
            edtID.setText(loginPreferences.getString("username", ""));
            edtPassword.setText(loginPreferences.getString("password", ""));
            isRemember.setChecked(true);
        }
        btnLogin.setOnClickListener(this);
        if (isRemember.isChecked() && loginPreferences.getString("ID", "").length() > 0) {
            login();
        }
    }
    void showProgressBar(){
        myProgress = new ProgressDialog(this);
        myProgress.setTitle("Login...");
        myProgress.setMessage("Please wait...");
        myProgress.setCancelable(true);
        // Hiển thị Progress Bar
            myProgress.show();
    }

    public void onClick(View view) {
        if (view == btnLogin) {
            InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
            imm.hideSoftInputFromWindow(edtID.getWindowToken(), 0);

            username = edtID.getText().toString();
            password = edtPassword.getText().toString();

            if (isRemember.isChecked()) {
                loginPrefsEditor.putBoolean("saveLogin", true);
                loginPrefsEditor.putString("username", username);
                loginPrefsEditor.putString("password", password);
                loginPrefsEditor.commit();
            } else {
                loginPrefsEditor.clear();
                loginPrefsEditor.commit();
            }
            showProgressBar();
            login();
        }
    }

    void changeIntent() {
        Intent intent = new Intent(LoginActivity.this, MainActivity.class);
        user = new User(Integer.parseInt(loginPreferences.getString("ID", "")), loginPreferences.getString("username", ""));
        startActivity(intent);
        LoginActivity.this.finish();
    }

    void login() {
        fetchData(new Listener() {
            @Override
            public void onResult(User user1) {
                user = user1;
                if (user == null) {
                    Toast toast = Toast.makeText(LoginActivity.this, "Your Password or Username is wrong", Toast.LENGTH_SHORT);
                    toast.show();
                } else {
                    if (!user.isShipper()) {
                        Toast toast = Toast.makeText(LoginActivity.this, "This app only support shipper", Toast.LENGTH_SHORT);
                        toast.show();
                    } else {
                        loginPrefsEditor.putString("ID", String.valueOf(user.getID()));
                        loginPrefsEditor.commit();
                        Intent intent = new Intent(LoginActivity.this, MainActivity.class);
                        myProgress.dismiss();
                        startActivity(intent);
                        LoginActivity.this.finish();
//            }
                    }
                }
            }
        });
    }

    private void fetchData(final Listener listener) {
        mUserAPI = RetrofitUtils.get().create(UserAPI.class);
        mUserAPI.login(edtID.getText().toString(), md5(edtPassword.getText().toString())).enqueue(new Callback<User>() {
            @Override
            public void onResponse(Call<User> call, Response<User> response) {
                listener.onResult(response.body());
            }

            @Override
            public void onFailure(Call<User> call, Throwable t) {
                Log.d("sai roi kia", t.getMessage());
            }
        });
    }

    public static final String md5(final String s) {
        final String MD5 = "MD5";
        try {
            // Create MD5 Hash
            MessageDigest digest = java.security.MessageDigest
                    .getInstance(MD5);
            digest.update(s.getBytes());
            byte messageDigest[] = digest.digest();

            // Create Hex String
            StringBuilder hexString = new StringBuilder();
            for (byte aMessageDigest : messageDigest) {
                String h = Integer.toHexString(0xFF & aMessageDigest);
                while (h.length() < 2)
                    h = "0" + h;
                hexString.append(h);
            }
            return hexString.toString();

        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        }
        return "";
    }
}
