package de.inter.versicherung.haus.automatisierung;

public class Thermostat {
    private static final int MAX_TEMP = 45;
    private static final int MIN_TEMP = -18;
    private boolean isOn;
    private int temperature;
    private ThermostatMetric thermostatMetric;

    public Thermostat() {
        isOn = false;
        temperature = 25;
        thermostatMetric = ThermostatMetric.METRIC;
    }

    public void toggleOnState(){
        isOn = !isOn;
    }

    public boolean getState(){
        return isOn;
    }

    public void increaseTemperatureByOne(){
        if(temperature < MAX_TEMP){
            temperature = temperature + 1;
        }
    }

    public void decreaseTemperatureByOne(){
        if(temperature > MIN_TEMP){
            temperature = temperature - 1;
        }
    }

    public int getTemperature(){
        return temperature;
    }

    @Override
    public String toString() {
        return "Thermostat{" +
                "isOn=" + isOn +
                ", temperature=" + temperature +
                ", thermostatMetric=" + thermostatMetric +
                '}';
    }
}
