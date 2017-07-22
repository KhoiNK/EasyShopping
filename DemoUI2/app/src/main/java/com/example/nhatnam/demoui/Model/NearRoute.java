package com.example.nhatnam.demoui.Model;

import java.util.ArrayList;
import java.util.List;


/**
 * Created by NHAT NAM on 7/22/2017.
 */

public class NearRoute {
    private List<ListPoint> arrayPoint;
    private List<Point> passPoint;

    public NearRoute() {
        this.arrayPoint = new ArrayList<>();
        this.passPoint = new ArrayList<>();
    }

    public List<Point> getPassPoint() {
        return passPoint;
    }

    public void calculateDistance() {
        for (int i = 0; i < arrayPoint.size(); i++) {
            arrayPoint.get(i).determineDistance();
        }
//        for (ListPoint listPoint:arrayPoint){
//            listPoint.determineDistance();
//        }
    }


    public void clear(){
//        arrayPoint= new ArrayList<>();
        arrayPoint.removeAll(arrayPoint);
        passPoint.removeAll(passPoint);
    }
    //add a point that know from location
    public void add(Point point) {
        if (arrayPoint.size() > 0) {
            for (int i = 0; i < arrayPoint.size(); i++) {
//                arrayPoint.get(i).removeLast();
                arrayPoint.get(i).add(new Point(arrayPoint.get(i).getList().get(0).getFrom(), point.getFrom()));
//                arrayPoint.get(i).add(new Point(arrayPoint.get(i).getList().get(0).getFrom(),point.getFrom()));
            }

            ListPoint newListPoint = new ListPoint();
            int maxColum = arrayPoint.get(0).getList().size();
            int maxRow = arrayPoint.size();
            for (int i = 0; i < maxColum; i++) {
                //point(from, destination)
                Point newPoint = new Point(point.getFrom(), arrayPoint.get(0).getList().get(i).getDestination());
                newListPoint.add(newPoint);
            }

//            newListPoint.getList().get(max-1).isEnable=false;
//            if(maxRow%2==0){
//                newListPoint.isEnable=true;
//            }
            //test
//            newListPoint.determineDistance();
            arrayPoint.add(newListPoint);

        } else {
            point.setDestination(point.getFrom());
            point.setDistance(0);
            point.isEnable = false;
            ListPoint newListPoint = new ListPoint();
            newListPoint.add(point);

//            newListPoint.add(point);
            arrayPoint.add(newListPoint);
        }

        int maxColum = arrayPoint.get(0).getList().size();
        int maxRow = arrayPoint.size();
        for (int i = 0; i < maxRow; i++) {
            for (int y = 0; y < maxColum; y++) {
                if (i != y && y % 2 == 0) {
                    arrayPoint.get(i).getList().get(y).isEnable = true;
                }
                if (i % 2 != 0) {
                    arrayPoint.get(i).getList().get(i - 1).isEnable = false;
                }
                if (arrayPoint.size() > 1) {
                    if (i != 1) {
                        arrayPoint.get(i).getList().get(1).isEnable = true;
                    }
                }
            }
        }


    }

    private int Min(ListPoint listPoint) {
        double MIN = 99999999;
        int Position = 0;
        int size = listPoint.getList().size();
        for (int i = 0; i < size; i++) {
            if (listPoint.getList().get(i).getDistance() < MIN && listPoint.getList().get(i).isEnable) {
                Position = i;
                MIN=listPoint.getList().get(i).getDistance();
            }
        }
        return Position;
    }

    private void recheckPassed(int id) {
        for (ListPoint listPoint : arrayPoint) {
            listPoint.getList().get(id).isEnable = false;
        }
    }

    private void checkEnable(int id) {
        for (ListPoint listPoint : arrayPoint) {
            listPoint.getList().get(id).isEnable = true;
        }
        arrayPoint.get(id).getList().get(id).isEnable=false;
    }

    public void findNearRoute() {
        int max = arrayPoint.size();
        int nowStand = 0;
        int nextStep;
        while (max > 0) {
            nextStep = Min(arrayPoint.get(nowStand));
            passPoint.add(arrayPoint.get(nowStand).getList().get(nextStep));
            recheckPassed(nextStep);
            if(nextStep%2==0){
                checkEnable(nextStep+1);
            }
            nowStand = nextStep;
            max--;
        }
    }

    public List<ListPoint> getArrayPoint() {
        return arrayPoint;
    }
}
