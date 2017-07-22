package com.example.nhatnam.demoui.Presenter.Activity;

import android.Manifest;
import android.app.NotificationManager;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.graphics.Color;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.media.RingtoneManager;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.support.design.widget.NavigationView;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.app.NotificationCompat;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.bumptech.glide.Glide;
import com.example.nhatnam.demoui.Model.API.DirectionFinderListener;
import com.example.nhatnam.demoui.Model.API.ShippingAPI;
import com.example.nhatnam.demoui.Model.API.ShopAPI;
import com.example.nhatnam.demoui.Model.API.Utils.RetrofitUtils;
import com.example.nhatnam.demoui.Model.NearRoute;
import com.example.nhatnam.demoui.Model.Order;
import com.example.nhatnam.demoui.Model.Point;
import com.example.nhatnam.demoui.Model.Route;
import com.example.nhatnam.demoui.Model.Shop;
import com.example.nhatnam.demoui.Model.User;
import com.example.nhatnam.demoui.Presenter.Activity.Adapter.ShopAdapter;
import com.example.nhatnam.demoui.Presenter.Activity.Dialog.CustomDialogClass;
import com.example.nhatnam.demoui.R;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.CameraPosition;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.gms.maps.model.Polyline;
import com.google.android.gms.maps.model.PolylineOptions;

import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener, LocationListener, DirectionFinderListener {
    GoogleMap map;
    public static User user;
    private static final int REQUEST_ID_ACCESS_COURSE_FINE_LOCATION = 100;
    private static final int TIME_REFRESH = 10;
    private static final int SEARCH_ACTIVITY = 1;
    private static final int LOGIN_ACTIVITY = 0;
    private static final int REQUEST_ACTIVITY = 2;
    private static final String BACK_GROUND = "https://scontent.fsgn2-2.fna.fbcdn.net/v/t35.0-12/20257459_840147529481194_60797793_o.jpg?oh=c43fbfc18074e4685490c3f9912f97c1&oe=59751988";
    private static final int FIND_NEAR = 1;
    private static final int SEARCH = 2;

    public static int STATUS = 0;
    private ProgressDialog myProgress;
    private static final String MYTAG = "MYTAG";
    private List<Shop> listshops = new ArrayList<>();
    private List<Shop> updatedList = new ArrayList<>();
    private ShopAdapter shopAdapter;
    private ShopAPI mShopAPI;
    private TextView txtUsername;
    private int NUM;
    private SharedPreferences loginPreferences;
    private SharedPreferences.Editor loginPrefsEditor;
    private List<Marker> originMarkers = new ArrayList<>();
    private List<Marker> destinationMarkers = new ArrayList<>();
    private List<Polyline> polylinePaths = new ArrayList<>();
    private List<Polyline> mypolylinePaths = new ArrayList<>();
    private List<Marker> listMarkerShop = new ArrayList<>();
    private ProgressDialog progressDialog;
    private LatLng shopLatlng;
    public static String mDestination = null;
    public static Order mOrder = null;
    public static LatLng myLatlng;
    private int count = 0;
    private ImageView ivUser;
    private LinearLayout linearLayout;
    boolean doubleBackToExitPressedOnce = false;
    private NearRoute nearRoute= new NearRoute();
    public static boolean pass;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        CreateMap();
        user = LoginActivity.user;
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);


//        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
//        fab.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
//                        .setAction("Action", null).show();
//            }
//        });

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.setDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);

    }

    private void showShopOnMap() {
        if (STATUS == SEARCH_ACTIVITY) {
            ShopAdapter.STATUS = SEARCH;
            searchFromandTo();
        } else {
            ShopAdapter.STATUS = FIND_NEAR;
            getShop();
        }
//        checkApproved();
    }

    private void searchFromandTo() {
        Bundle bundle = getIntent().getExtras();

        int fromCity = bundle.getInt("fromCity");
        int fromDistrict = bundle.getInt("fromDistrict");
        int toCity = bundle.getInt("toCity");
        int toDistrict = bundle.getInt("toDistrict");
        searchShop(fromCity, fromDistrict, toCity, toDistrict);
    }

    private void searchOrdersSameDestination(int toCity, int toDistrict) {
        ShopAdapter.STATUS = SEARCH;
        searchShop(0, 0, toCity, toDistrict);
    }

    @Override
    public void onBackPressed() {
        //remove polyline on map
        if (listMarkerShop != null) {
            for (Polyline polyline : mypolylinePaths) {
                polyline.remove();
            }
            //reload map
            showShopOnMap();
        }
        if (destinationMarkers != null) {
            for (Marker marker : destinationMarkers)
                marker.remove();
        }
        if (originMarkers != null) {
            for (Marker marker : originMarkers)
                marker.remove();
        }
// double press on back key to close app
        if (getSupportFragmentManager().getBackStackEntryCount() > 0) {
            getSupportFragmentManager().popBackStack();
        } else if (!doubleBackToExitPressedOnce) {
            this.doubleBackToExitPressedOnce = true;
            Toast.makeText(this, "Please click BACK again to exit.", Toast.LENGTH_SHORT).show();

            new Handler().postDelayed(new Runnable() {

                @Override
                public void run() {
                    doubleBackToExitPressedOnce = false;
                }
            }, 2000);
        } else {
            super.onBackPressed();
            return;
        }
//        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
//        if (drawer.isDrawerOpen(GravityCompat.START)) {
//            drawer.closeDrawer(GravityCompat.START);
//        } else {
//            new AlertDialog.Builder(this)
//                    .setIcon(android.R.drawable.ic_dialog_alert)
//                    .setTitle("Closing Activity")
//                    .setMessage("Are you sure you want to close this activity?")
//                    .setPositiveButton("Yes", new DialogInterface.OnClickListener() {
//                        @Override
//                        public void onClick(DialogInterface dialog, int which) {
//                            finish();
//                        }
//
//                    })
//                    .setNegativeButton("No", null)
//                    .show();
//        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        getUser();
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.Approved) {
            Intent intent = new Intent(this, ApprovedActivity.class);
            startActivity(intent);
        }
//        else if (id == R.id.nav_request) {
//            Intent intent = new Intent(this, RequestActivity.class);
//            startActivity(intent);
//        }
        else if (id == R.id.nav_profile) {
            Intent intent = new Intent(this, ProfileActivity.class);
            startActivity(intent);
        } else if (id == R.id.nav_logout) {
            loginPreferences = getSharedPreferences("loginPrefs", MODE_PRIVATE);
            loginPrefsEditor = loginPreferences.edit();
            loginPrefsEditor.clear();
            loginPrefsEditor.commit();
            Intent intent = new Intent(this, LoginActivity.class);
            startActivity(intent);
            this.finish();
        } else if (id == R.id.nav_search) {
            try {
                Intent intent = new Intent(this, SearchActivity.class);
                startActivity(intent);
            } catch (Exception e) {
                Log.d("loi chuyen intent", e.getMessage());
            }
        }else if (id == R.id.nav_search_way) {
            nearRoute.findNearRoute();
        }
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }


    public void CreateMap() {
        // Tạo Progress Bar
        myProgress = new ProgressDialog(this);
        myProgress.setTitle("Map Loading ...");
        myProgress.setMessage("Please wait...");
        myProgress.setCancelable(true);

        // Hiển thị Progress Bar
        if (STATUS == LOGIN_ACTIVITY) {
            myProgress.show();
        }

        SupportMapFragment mapFragment
                = (SupportMapFragment) getSupportFragmentManager().findFragmentById(R.id.map);
        // Sét đặt sự kiện thời điểm GoogleMap đã sẵn sàng.
        mapFragment.getMapAsync(new OnMapReadyCallback() {

            @Override
            public void onMapReady(GoogleMap googleMap) {
                onMyMapReady(googleMap);
            }
        });
    }

    private void onMyMapReady(GoogleMap googleMap) {

        // Lấy đối tượng Google Map ra:
        map = googleMap;

        // Thiết lập sự kiện đã tải Map thành công
        map.setOnMapLoadedCallback(new GoogleMap.OnMapLoadedCallback() {

            @Override
            public void onMapLoaded() {

                // Đã tải thành công thì tắt Dialog Progress đi
                myProgress.dismiss();

                // Hiển thị vị trí người dùng.
                askPermissionsAndShowMyLocation();
            }
        });
        map.setMapType(GoogleMap.MAP_TYPE_NORMAL);
        map.getUiSettings().setZoomControlsEnabled(true);

        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED
                || ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {

            ActivityCompat.requestPermissions(MainActivity.this,
                    new String[]{Manifest.permission.ACCESS_FINE_LOCATION,
                            Manifest.permission.ACCESS_COARSE_LOCATION}, REQUEST_ID_ACCESS_COURSE_FINE_LOCATION);


        } else {
            map.setMyLocationEnabled(true);
        }

    }


    private void askPermissionsAndShowMyLocation() {


        // Với API >= 23, bạn phải hỏi người dùng cho phép xem vị trí của họ.
        if (Build.VERSION.SDK_INT >= 23) {
            int accessCoarsePermission
                    = ContextCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION);
            int accessFinePermission
                    = ContextCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION);


            if (accessCoarsePermission != PackageManager.PERMISSION_GRANTED
                    || accessFinePermission != PackageManager.PERMISSION_GRANTED) {

                // Các quyền cần người dùng cho phép.
                String[] permissions = new String[]{Manifest.permission.ACCESS_COARSE_LOCATION,
                        Manifest.permission.ACCESS_FINE_LOCATION};

                // Hiển thị một Dialog hỏi người dùng cho phép các quyền trên.
                ActivityCompat.requestPermissions(this, permissions,
                        REQUEST_ID_ACCESS_COURSE_FINE_LOCATION);

                return;
            }
        }

        // Hiển thị vị trí hiện thời trên bản đồ.
        this.showMyLocation();
        showShopOnMap();
    }


    // Khi người dùng trả lời yêu cầu cấp quyền (cho phép hoặc từ chối).

    @Override
    public void onRequestPermissionsResult(int requestCode,
                                           String permissions[], int[] grantResults) {

        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        //
        switch (requestCode) {
            case REQUEST_ID_ACCESS_COURSE_FINE_LOCATION: {


                // Chú ý: Nếu yêu cầu bị bỏ qua, mảng kết quả là rỗng.
                if (grantResults.length > 1
                        && grantResults[0] == PackageManager.PERMISSION_GRANTED
                        && grantResults[1] == PackageManager.PERMISSION_GRANTED) {

                    Toast.makeText(this, "Permission granted!", Toast.LENGTH_LONG).show();

                    // Hiển thị vị trí hiện thời trên bản đồ.
                    this.showMyLocation();
                }
                // Hủy bỏ hoặc từ chối.
                else {
                    Toast.makeText(this, "Permission denied!", Toast.LENGTH_LONG).show();
                }
                break;
            }
        }
    }

    // Tìm một nhà cung cấp vị trị hiện thời đang được mở.
    private String getEnabledLocationProvider() {
        LocationManager locationManager = (LocationManager) getSystemService(LOCATION_SERVICE);


        // Tiêu chí để tìm một nhà cung cấp vị trí.
        Criteria criteria = new Criteria();

        // Tìm một nhà cung vị trí hiện thời tốt nhất theo tiêu chí trên.
        // ==> "gps", "network",...
        String bestProvider = locationManager.getBestProvider(criteria, true);

        boolean enabled = locationManager.isProviderEnabled(bestProvider);

        if (!enabled) {
            Toast.makeText(this, "No location provider enabled!", Toast.LENGTH_LONG).show();
            Log.i(MYTAG, "No location provider enabled!");
            return null;
        }
        return bestProvider;
    }

    // Chỉ gọi phương thức này khi đã có quyền xem vị trí người dùng.
    private void showMyLocation() {

        LocationManager locationManager = (LocationManager) getSystemService(LOCATION_SERVICE);

        String locationProvider = this.getEnabledLocationProvider();

        if (locationProvider == null) {
            return;
        }

        // Millisecond
        final long MIN_TIME_BW_UPDATES = 1000;
        // Met
        final float MIN_DISTANCE_CHANGE_FOR_UPDATES = 1;

        Location myLocation = null;
        try {

            // Đoạn code nay cần người dùng cho phép (Hỏi ở trên ***).
            locationManager.requestLocationUpdates(
                    locationProvider,
                    MIN_TIME_BW_UPDATES,
                    MIN_DISTANCE_CHANGE_FOR_UPDATES, (LocationListener) this);

            // Lấy ra vị trí.
            myLocation = locationManager
                    .getLastKnownLocation(locationProvider);
        }
        // Với Android API >= 23 phải catch SecurityException.
        catch (SecurityException e) {
            Toast.makeText(this, "Show My Location Error: " + e.getMessage(), Toast.LENGTH_LONG).show();
            Log.e(MYTAG, "Show My Location Error:" + e.getMessage());
            e.printStackTrace();
            return;
        }

        if (myLocation != null) {

            myLatlng = new LatLng(myLocation.getLatitude(), myLocation.getLongitude());
            map.animateCamera(CameraUpdateFactory.newLatLngZoom(myLatlng, 13));

            CameraPosition cameraPosition = new CameraPosition.Builder()
                    .target(myLatlng)             // Sets the center of the map to location user
                    .zoom(17)                   // Sets the zoom
                    .bearing(90)                // Sets the orientation of the camera to east
                    .tilt(40)                   // Sets the tilt of the camera to 30 degrees
                    .build();                   // Creates a CameraPosition from the builder
            map.animateCamera(CameraUpdateFactory.newCameraPosition(cameraPosition));

        } else {
            Toast.makeText(this, "Location not found!", Toast.LENGTH_LONG).show();
            Log.i(MYTAG, "Location not found");
        }


    }

    public void refreshLocation() {
        LocationManager locationManager = (LocationManager) getSystemService(LOCATION_SERVICE);

        String locationProvider = this.getEnabledLocationProvider();

        if (locationProvider == null) {
            return;
        }

        // Millisecond
        final long MIN_TIME_BW_UPDATES = 1000;
        // Met
        final float MIN_DISTANCE_CHANGE_FOR_UPDATES = 1;

        Location myLocation = null;
        try {

            // Đoạn code nay cần người dùng cho phép (Hỏi ở trên ***).
            locationManager.requestLocationUpdates(
                    locationProvider,
                    MIN_TIME_BW_UPDATES,
                    MIN_DISTANCE_CHANGE_FOR_UPDATES, (LocationListener) this);

            // Lấy ra vị trí.
            myLocation = locationManager
                    .getLastKnownLocation(locationProvider);
        }
        // Với Android API >= 23 phải catch SecurityException.
        catch (SecurityException e) {
            Toast.makeText(this, "Show My Location Error: " + e.getMessage(), Toast.LENGTH_LONG).show();
            Log.e(MYTAG, "Show My Location Error:" + e.getMessage());
            e.printStackTrace();
            return;
        }

        if (myLocation != null) {

            myLatlng = new LatLng(myLocation.getLatitude(), myLocation.getLongitude());
        }
    }


    public void addMarker() {
//        // test ShopAdapter
//        Shop shop1 = new Shop("1234",new LatLng(16.075842, 108.170096),"shop a");
//        listshops.add(shop1);
//        Shop shop2 = new Shop("w523",new LatLng(16.075182, 108.169659),"shop b");
//        listshops.add(shop2);
//        Shop shop3 = new Shop("sa123",new LatLng(16.076105, 108.169402),"shop c");
//        listshops.add(shop3);
//        lvOrders= (ListView) findViewById(R.id.list_orders);
//        List<Order> mOrders=new ArrayList<Order>();
//        //shop a
//        Order order1 = new Order("16.075642, 108.169785","http://vn-live-01.slatic.net/p/2/xiaomi-mi5-64gb-hang-phan-phoi-chinh-thuc-6091-1583484-cfb24bed17cb6e516b08d96967610a5e-webp-product.jpg",15000,"Hai Phong");
//        mOrders.add(order1);
//        Order order2 = new Order("16.075642, 108.169785","http://vn-live-01.slatic.net/p/2/xiaomi-mi5-64gb-hang-phan-phoi-chinh-thuc-6091-1583484-cfb24bed17cb6e516b08d96967610a5e-webp-product.jpg",155000,"Ha Noi");
//        mOrders.add(order2);
//        Order order3 = new Order("16.075642, 108.169785","http://vn-live-01.slatic.net/p/2/xiaomi-mi5-64gb-hang-phan-phoi-chinh-thuc-6091-1583484-cfb24bed17cb6e516b08d96967610a5e-webp-product.jpg",25000,"Da Nang");
//        mOrders.add(order3);
//        listshops.get(0).setOrders(mOrders);
//        mOrders.clear();
//        //
//        //shop b
//        Order order4 = new Order("16.075642, 108.169785","http://vn-live-02.slatic.net/p/8/balo-laptop-glado-cylinder-mau-xam-blc006-hang-phan-phoi-chinhthuc-6923-2506462-7a83414d088f45e27ccee6cb3d55c455-webp-product.jpg",15000,"Hai Phong");
//        mOrders.add(order4);
//        Order order5 = new Order("16.075642, 108.169785","http://vn-live-02.slatic.net/p/8/balo-laptop-glado-cylinder-mau-xam-blc006-hang-phan-phoi-chinhthuc-6923-2506462-7a83414d088f45e27ccee6cb3d55c455-webp-product.jpg",155000,"Ha Noi");
//        mOrders.add(order5);
//        Order order6 = new Order("16.075642, 108.169785","http://vn-live-02.slatic.net/p/8/balo-laptop-glado-cylinder-mau-xam-blc006-hang-phan-phoi-chinhthuc-6923-2506462-7a83414d088f45e27ccee6cb3d55c455-webp-product.jpg",25000,"Da Nang");
//        mOrders.add(order6);
//        listshops.get(1).setOrders(mOrders);
//        mOrders.clear();
//        //
//        //shop c
//        Order order7 = new Order("16.075642, 108.169785","http://vn-live-02.slatic.net/p/3/bo-noi-anod-sunhouse-3-chiec-quai-eb-sh8831ebsh8831eb-phi-18-22-26vang-1030-1891531-4d04c77434bb490f2185815d7468f533-webp-zoom.jpg",15000,"Hai Phong");
//        mOrders.add(order7);
//        Order order8 = new Order("16.075642, 108.169785","http://vn-live-02.slatic.net/p/3/bo-noi-anod-sunhouse-3-chiec-quai-eb-sh8831ebsh8831eb-phi-18-22-26vang-1030-1891531-4d04c77434bb490f2185815d7468f533-webp-zoom.jpg",155000,"Ha Noi");
//        mOrders.add(order8);
//        Order order9 = new Order("16.075642, 108.169785","http://vn-live-02.slatic.net/p/3/bo-noi-anod-sunhouse-3-chiec-quai-eb-sh8831ebsh8831eb-phi-18-22-26vang-1030-1891531-4d04c77434bb490f2185815d7468f533-webp-zoom.jpg",25000,"Da Nang");
//        mOrders.add(order9);
//        listshops.get(2).setOrders(mOrders);
//        mOrders.clear();
//        //

        shopAdapter = new ShopAdapter(listshops);
        listMarkerShop = shopAdapter.addMaker(map, listMarkerShop);
        //
//        map.addMarker(new MarkerOptions()
//                .position(new LatLng(16.075642, 108.169785)));

        map.setOnMarkerClickListener(new GoogleMap.OnMarkerClickListener() {
            @Override
            public boolean onMarkerClick(final Marker marker) {
                ;
//                lvOrders = (ListView) findViewById(R.id.list_orders);
                //dialog
                int i = shopAdapter.findShopByLatlng(marker.getPosition());
                if (i >= 0) {
                    shopLatlng = marker.getPosition();
                    CustomDialogClass cdd = new CustomDialogClass(MainActivity.this
                            , listshops.get(shopAdapter.findShopByLatlng(marker.getPosition())).getListOrder());
                    cdd.setCdd(cdd);
                    cdd.setOnDismissListener(new DialogInterface.OnDismissListener() {
                        @Override
                        public void onDismiss(DialogInterface dialogInterface) {
                            if (mDestination != null) {
                                pass=false;
                                nearRoute.add(new Point(shopLatlng.latitude + "," + shopLatlng.longitude));
                                nearRoute.add(new Point(mDestination));
                                nearRoute.calculateDistance();
//                                while (!pass){
////                                    nearRoute.calculateDistance();
//                                    pass=nearRoute.check();
//                                }
//                                nearRoute.calculateDistance();
//                                Handler h = new Handler();
//                                h.postDelayed(r, 1000);
                                findDirection(shopLatlng.latitude + "," + shopLatlng.longitude, mDestination);
                                searchOrdersSameDestination(mOrder.getCityID(), mOrder.getDistrictID());
                                mDestination = null;
                                mOrder = null;
                            }
                            if (checkShopByLatlng(shopLatlng)) {
                                marker.remove();
                            }
                        }
                    });
                    cdd.show();
                }
                return false;
            }
        });
    }
    Runnable r = new Runnable() {
        @Override
        public void run(){
//            doSomething(); //<-- put your code in here.
        }
    };
    public boolean checkShopByLatlng(LatLng latLng) {
        for (Shop shop : listshops) {
            if (shop.getListOrder().size() == 0) {
                return true;
            }
        }
        return false;
    }

    public void getShop() {
        listshops.clear();
        fecthDataShop(new SearchListener() {
            @Override
            public void onResult(List<Shop> shop) {
                listshops = shop;
                addMarker();
                if (STATUS == REQUEST_ACTIVITY) {
                    guild();
                }
            }
        }, 0, 0, 0, 0);
    }

    public void searchShop(int fromCity, int fromDistrict, int toCity, int toDistrict) {
        listshops.clear();
        mShopAPI = RetrofitUtils.get().create(ShopAPI.class);
        Call<List<Shop>> call = mShopAPI.getShops(user.getID(), fromCity, fromDistrict, toCity, toDistrict);
        call.enqueue(new Callback<List<Shop>>() {
            @Override
            public void onResponse(Call<List<Shop>> call, Response<List<Shop>> response) {
                listshops = response.body();
                addMarker();
                Log.d("List length", String.valueOf(listshops.size()));
                Log.d("success?:", String.valueOf(response.isSuccessful()));
            }

            @Override
            public void onFailure(Call<List<Shop>> call, Throwable t) {
                Log.d("loi roi: ", t.getMessage());

            }
        });
    }
//    public void searchShop(int fromCity, int fromDistrict, int toCity, int toDistrict) {
//        listshops.clear();
//        fecthDataShop(new SearchListener() {
//            @Override
//            public void onResult(List<Shop> shop) {
//                listshops = shop;
//                Log.d("Search list size", String.valueOf(listshops.size()));
//                addMarker();
//            }
//        }, fromCity, fromDistrict, toCity, toDistrict);
//    }

    public void fecthDataShop(final SearchListener listener, int FromProvince, int FromDistrict, int ToProvince, int ToDistrict) {
        mShopAPI = RetrofitUtils.get().create(ShopAPI.class);
        Call<List<Shop>> call = mShopAPI.getShops(user.getID(), FromProvince, FromDistrict, ToProvince, ToDistrict);
        call.enqueue(new Callback<List<Shop>>() {
            @Override
            public void onResponse(Call<List<Shop>> call, Response<List<Shop>> response) {
                listener.onResult(response.body());
                Log.d("List length", String.valueOf(listshops.size()));
                Log.d("success?:", String.valueOf(response.isSuccessful()));
            }

            @Override
            public void onFailure(Call<List<Shop>> call, Throwable t) {
                Log.d("loi roi: ", t.getMessage());

            }
        });

    }

    @Override
    public void onLocationChanged(Location location) {

//        count++;
//        if (count == TIME_REFRESH) {
//        checkApproved();
        refreshLocation();
//        mShopAPI = RetrofitUtils.get().create(ShopAPI.class);
//        Call<List<Shop>> call = mShopAPI.getShops(user.getID(), 0, 0, 0, 0);
//        call.enqueue(new Callback<List<Shop>>() {
//            @Override
//            public void onResponse(Call<List<Shop>> call, Response<List<Shop>> response) {
//                updatedList = response.body();
//                if (updatedList.size() != listshops.size() || updatedList.containsAll(listshops) == false) {
//                    listshops = updatedList;
//                    addMarker();
//                    count = 0;
////                        notifyShow();
//                }
//            }
//
//            @Override
//            public void onFailure(Call<List<Shop>> call, Throwable t) {
//                Log.d("loi roi: ", t.getMessage());
//
//            }
//        });
////        }
    }

    public void checkApproved() {
        ShippingAPI mShipping = RetrofitUtils.get().create(ShippingAPI.class);
        mShipping.getNumApproved(user.getID()).enqueue(new Callback<Integer>() {
            @Override
            public void onResponse(Call<Integer> call, Response<Integer> response) {
                NUM = response.body();
                loginPreferences = getSharedPreferences("loginPrefs", MODE_PRIVATE);
                loginPrefsEditor = loginPreferences.edit();
                int mNum = loginPreferences.getInt("NUM", 0);

                if (mNum > 0) {
                    if (mNum != NUM) {
                        notifyShow();
                        loginPrefsEditor.putInt("NUM", NUM);
                        loginPrefsEditor.commit();
                    }
                } else {
                    loginPrefsEditor.putInt("NUM", NUM);
                    loginPrefsEditor.commit();
                }

            }

            @Override
            public void onFailure(Call<Integer> call, Throwable t) {

            }
        });
    }

    private void notifyShow() {
        NotificationCompat.Builder mBuilder =
                (NotificationCompat.Builder) new NotificationCompat.Builder(this)
                        .setSmallIcon(R.drawable.logo_es)
                        .setContentTitle("My notification")
                        .setContentText("Hello World!");
        mBuilder.setSound(RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION));
        NotificationManager notificationManager = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);
        notificationManager.notify(1, mBuilder.build());
    }

    @Override
    public void onStatusChanged(String provider, int status, Bundle extras) {

    }

    @Override
    public void onProviderEnabled(String provider) {

    }

    @Override
    public void onProviderDisabled(String provider) {

    }

    void getUser() {
        Bundle bundle = getIntent().getExtras();
        txtUsername = (TextView) findViewById(R.id.textView2);
        linearLayout = (LinearLayout) findViewById(R.id.nav_header_background);
        ivUser = (ImageView) findViewById(R.id.nav_header_profile_image);
        txtUsername.setText(user.getFirstName());
        if (user.getUserImage() != null) {
            Glide.with(this)
                    .load(user.getUserImage())
                    .into(ivUser);
        }

    }

    private interface SearchListener {
        void onResult(List<Shop> shop);
    }

    @Override
    protected void onResume() {
        super.onResume();
    }


    @Override
    public void onDirectionFinderStart() {
        progressDialog = ProgressDialog.show(this, "Please wait.",
                "Finding direction..!", true);
//
        if (originMarkers != null) {
            for (Marker marker : originMarkers) {
                marker.remove();
            }
        }

        if (destinationMarkers != null) {
            for (Marker marker : destinationMarkers) {
                marker.remove();
            }
        }

        if (mypolylinePaths != null) {
            for (Polyline polyline : mypolylinePaths) {
                polyline.remove();
            }
//            polylinePaths.clear();
//            Polyline polyline=
        }
    }

    @Override
    public void onDirectionFinderSuccess(List<Route> routes) {
        progressDialog.dismiss();
        if (polylinePaths == null) {
            polylinePaths = new ArrayList<>();
        }
        if (originMarkers == null) {
            originMarkers = new ArrayList<>();
        }
        if (destinationMarkers == null) {
            destinationMarkers = new ArrayList<>();
        }
        for (Route route : routes) {
            CameraPosition cameraPosition = new CameraPosition.Builder()
                    .target(myLatlng)             // Sets the center of the map to location user
                    .zoom(17)                   // Sets the zoom
                    .bearing(90)                // Sets the orientation of the camera to east
                    .tilt(40)                   // Sets the tilt of the camera to 30 degrees
                    .build();

            destinationMarkers.add(map.addMarker(new MarkerOptions()
                    .icon(BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_ORANGE))
                    .title(route.endAddress)
                    .position(route.endLocation)));
            Toast toast = Toast.makeText(MainActivity.this, "Distance: " + route.distance.text + "    Duration: " + route.duration.text, Toast.LENGTH_LONG);
            toast.show();
            PolylineOptions polylineOptions = new PolylineOptions().
                    geodesic(true).
                    color(Color.BLUE).
                    width(15);

            for (int i = 0; i < route.points.size(); i++)
                polylineOptions.add(route.points.get(i));

            polylinePaths.add(map.addPolyline(polylineOptions));
        }
        mypolylinePaths.addAll(polylinePaths);
    }

    public void findDirection(String origin, String destination) {

        try {
            new DirectionFinder(this, origin, destination, myLatlng.latitude + "," + myLatlng.longitude).execute();
        } catch (UnsupportedEncodingException e) {
            e.printStackTrace();
        }
    }

    public void guild() {
        try {
            Bundle bundle = getIntent().getExtras();
            String Lat = String.valueOf(bundle.getDouble("Lat"));
            String Lng = String.valueOf(bundle.getDouble("Lng"));
            String Address = bundle.getString("Address");
            if (Lat != null && Lng != null && Address != null) {
                findDirection(Lat + "," + Lng, Address);
            }
        } catch (Exception e) {
            Log.d("loi guild", e.getMessage());
        }


    }
}
