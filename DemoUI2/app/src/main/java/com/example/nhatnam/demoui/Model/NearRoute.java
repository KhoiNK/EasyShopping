package com.example.nhatnam.demoui.Model;

import java.util.ArrayList;
import java.util.List;


/**
 * Created by NHAT NAM on 7/22/2017.
 */

public class NearRoute {
    private List<ListPoint> arrayPoint;
    private List<Point> passPoint;
    private List<List<Point>> listResult;
//    private List<Point> Result;
//    private double Max= Double.MAX_VALUE;

    public NearRoute() {
        this.arrayPoint = new ArrayList<>();
        this.passPoint = new ArrayList<>();
        this.listResult = new ArrayList<>();
    }

    public List<Point> getPassPoint() {
        return passPoint;
    }

    public void calculateDistance() {
        List<ListPoint> a = new ArrayList<>(arrayPoint);
        for (int i = 0; i < a.size(); i++) {
            a.get(i).determineDistance();
        }
//        for (ListPoint listPoint:arrayPoint){
//            listPoint.determineDistance();
//        }
    }


    public void clear() {
//        arrayPoint= new ArrayList<>();
        arrayPoint.removeAll(arrayPoint);
        passPoint.removeAll(passPoint);
    }

    //add a point that know from location
    public void add(Point point) {
        if (arrayPoint.size() > 0) {
            for (int i = 0; i < arrayPoint.size(); i++) {
                arrayPoint.get(i).add(new Point(arrayPoint.get(i).getList().get(0).getFrom(), point.getFrom()));
            }

            ListPoint newListPoint = new ListPoint();
            int maxColum = arrayPoint.get(0).getList().size();
            int maxRow = arrayPoint.size();
            for (int i = 0; i < maxColum; i++) {
                //point(from, destination)
                Point newPoint = new Point(point.getFrom(), arrayPoint.get(0).getList().get(i).getDestination());
                newListPoint.add(newPoint);
            }

            arrayPoint.add(newListPoint);

        } else {
            point.setDestination(point.getFrom());
            point.setDistance(0);
            point.isEnable = false;
            ListPoint newListPoint = new ListPoint();
            newListPoint.add(point);

            arrayPoint.add(newListPoint);
        }

        int maxColum = arrayPoint.get(0).getList().size();
        int maxRow = arrayPoint.size();
        for (int i = 0; i < maxRow; i++) {
            for (int y = 0; y < maxColum; y++) {
                if (i != y && y % 2 == 0 && y > 0) {
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

    public void remove(Point shop, Point order) {
        int position = -1;
        for (int y = 0; y < arrayPoint.size() - 1; y++) {
            if (arrayPoint.get(0).getList().get(y).getDestination().equals(shop.getFrom())
                    && arrayPoint.get(0).getList().get(y + 1).getDestination().equals(order.getFrom())) {
                position = y;
                break;
            }
        }
        if (position >= 0) {
            arrayPoint.remove(position);
            arrayPoint.remove(position);

            for (ListPoint listPoint : arrayPoint) {
                listPoint.getList().remove(position);
                listPoint.getList().remove(position);
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
                MIN = listPoint.getList().get(i).getDistance();
            }
        }
        return Position;
    }

    private void recheckPassed(int id, List<ListPoint> array) {
        for (ListPoint listPoint : array) {
            listPoint.getList().get(id).isEnable = false;
        }
    }

    private void checkEnable(int id, List<ListPoint> array) {
        for (ListPoint listPoint : array) {
            listPoint.getList().get(id).isEnable = true;
        }
        array.get(id).getList().get(id).isEnable = false;
    }

    public void findNearRoute() {
        List<ListPoint> a = Clone(arrayPoint);
        int max = a.size();
        int nowStand = 0;
        int nextStep;
        passPoint.clear();
        while (max > 1) {
            nextStep = Min(a.get(nowStand));
            passPoint.add(a.get(nowStand).getList().get(nextStep));
            recheckPassed(nextStep, a);
            if (nextStep % 2 == 0) {
                checkEnable(nextStep + 1, a);
            }
            nowStand = nextStep;
            max--;
        }

    }

    private List<ListPoint> Clone(List<ListPoint> array) {
        List<ListPoint> clone = new ArrayList<>();
        for (ListPoint listPoint : array) {
            clone.add(new ListPoint(listPoint.Clone()));
        }
        return clone;
    }

//    public void theway(int step, List<ListPoint> a, List<Point> result) {
//        for (int i = 1; i < a.size(); i++) {
//            List<Point> mResult = new ArrayList<>();
//            for (Point point : result) {
//                mResult.add(new Point(point));
//            }
//            if (a.get(step).getList().get(i).isEnable) {
//                mResult.add(a.get(step).getList().get(i));
//                if (mResult.size() == a.size() - 1) {
//                    listResult.add(mResult);
//                } else {
//                    recheckPassed(i, a);
//                    if (i % 2 == 0) {
//                        checkEnable(i + 1, a);
//                    }
//                    theway(i, Clone(a), mResult);
//                }
//            }
//
//        }
//
//    }

    public void theway(int step, List<ListPoint> a, List<Point> result) {
        for (int i = 1; i < a.size(); i++) {
            List<Point> mResult = new ArrayList<>();
            //clone result
            for (Point point : result) {
                mResult.add(new Point(point));
            }
            //check neu no chua co trong result thi moi lam
            if (checkShop(mResult, a.get(step).getList().get(i))) {
                //neu le thi phai di qua shop cua order roi thi moi add vo result
                if (i % 2 == 1 && checkOrder(mResult, a.get(step).getList().get(i - 1))) {
                    mResult.add(a.get(step).getList().get(i));
                    if (mResult.size() == a.size()) {
                        listResult.add(mResult);
//                        if(SumDis(mResult)>Min)
                    } else {
                        theway(i, a, mResult);
                    }
                }
                //neu chan thi no la shop se add vo result
                if (i % 2 == 0) {
                    mResult.add(a.get(step).getList().get(i));
                    if (mResult.size() == a.size()) {
                        listResult.add(mResult);
                    } else {
                        theway(i, a, mResult);
                    }
                }
            }
        }

    }

    private double SumDis(List<Point> points){
        double sum=0;
        for(Point point:points){
            sum=sum=point.getDistance();
        }
        return sum;
    }
    public boolean checkShop(List<Point> Result, Point point) {
        for (Point point1 : Result) {
            if (point1.getDestination() == point.getDestination()) {
                return false;
            }
        }
        return true;
    }

    public boolean checkOrder(List<Point> Result, Point point) {
        for (Point point1 : Result) {
            if (point1.getDestination() == point.getDestination()) {
                return true;
            }
        }
        return false;
    }

    public List<Point> findtheway() {
        List<Point> Result = new ArrayList<>();
        listResult = new ArrayList<>();
        Result.add(arrayPoint.get(0).getList().get(0));
        theway(0, arrayPoint, Result);
        double Min = Double.MAX_VALUE;
        int Position = 0;
        for (int i = 0; i < listResult.size(); i++) {
            double Dis = 0;
            for (int y = 0; y < arrayPoint.size(); y++) {
                Dis = Dis + listResult.get(i).get(y).getDistance();
            }
            if (Dis < Min) {
                Position = i;
                Min = Dis;
            }
        }
        return listResult.get(Position).subList(1,arrayPoint.size());
    }


    public List<ListPoint> getArrayPoint() {
        return arrayPoint;
    }
}
