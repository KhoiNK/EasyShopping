package com.example.nhatnam.demoui.Model.API;

import com.example.nhatnam.demoui.Model.Route;

import java.util.List;

/**
 * Created by NHAT NAM on 7/10/2017.
 */

public interface DirectionFinderListener {
    void onDirectionFinderStart();
    void onDirectionFinderSuccess(List<Route> route);
}

