package com.example.nhatnam.demoui.Presenter.Activity;

import android.app.Activity;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.widget.ImageView;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.example.nhatnam.demoui.Model.API.UserAPI;
import com.example.nhatnam.demoui.Model.API.Utils.RetrofitUtils;
import com.example.nhatnam.demoui.Model.User;
import com.example.nhatnam.demoui.R;

import java.text.DecimalFormat;
import java.text.SimpleDateFormat;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by NHAT NAM on 7/21/2017.
 */

public class ProfileActivity extends Activity {
    private UserAPI mUserAPI;
    private TextView tvUsername;
    private TextView tvFullname;
    private TextView tvBirthday;
    private TextView tvStartdat;
    private TextView tvPhone;
    private TextView tvTotal;
    private TextView tvCurrentDeposit;
    private TextView tvDeposit;
    private ImageView ivUser;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.profile_activity);
        findview();
        loadUserProfile();
    }

    private void loadUserProfile(){
        fetchData(new Listener() {
            @Override
            public void onResult(User user) {
                bindData(user);
            }
        });
    }

    private void bindData(User user){
        //format double without decimal
        DecimalFormat format = new DecimalFormat();
        format.setDecimalSeparatorAlwaysShown(false);

        tvUsername.setText(user.getFirstName());
        tvFullname.setText(user.getLastName()+" "+user.getFirstName());
        if(user.getBirthDay()!=null){
            tvBirthday.setText(new SimpleDateFormat("E, MMM d, yyyy").format(user.getBirthDay()));
        }
        if(user.getStartDate()!=null){
            tvStartdat.setText(new SimpleDateFormat("E, MMM d, yyyy").format(user.getStartDate()));
        }
        if(user.getPhone()!=null){
            tvPhone.setText(user.getPhone());
        }
        tvTotal.setText(format.format(user.getTotal()));
        tvCurrentDeposit.setText(format.format(user.getCurrentDeposit()));
        tvDeposit.setText(format.format(user.getDeposit()));
        if(user.getUserImage()!=null){
            Glide.with(this)
                    .load(user.getUserImage())
                    .into(ivUser);
        }
    }
    private void fetchData(final Listener listener) {
        mUserAPI = RetrofitUtils.get().create(UserAPI.class);
        mUserAPI.getProfile(MainActivity.user.getID()).enqueue(new Callback<User>() {
            @Override
            public void onResponse(Call<User> call, Response<User> response) {
                listener.onResult(response.body());
            }

            @Override
            public void onFailure(Call<User> call, Throwable t) {

            }
        });
    }

    private interface Listener {
        void onResult(User user);
    }

    private void findview() {
        tvUsername = (TextView) findViewById(R.id.pfuser_name);
        tvFullname = (TextView) findViewById(R.id.pffullname);
        tvBirthday = (TextView) findViewById(R.id.pfdob);
        tvStartdat = (TextView) findViewById(R.id.pfstart_date);
        tvPhone = (TextView) findViewById(R.id.pfphone);
        tvTotal = (TextView) findViewById(R.id.pftotal);
        tvCurrentDeposit = (TextView) findViewById(R.id.pfcurrent_deposit);
        tvDeposit = (TextView) findViewById(R.id.pfdeposit);
        ivUser = (ImageView) findViewById(R.id.profile_image);
    }

}
