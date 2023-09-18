package de.inter.versicherung;

import de.inter.versicherung.haus.automatisierung.*;

public class BuildingManager {
    private final InputOutputHandler ioHandler;
    private Building building;

    public BuildingManager(InputOutputHandler ioHandler){
        this.ioHandler = ioHandler;
    }

    public void createBuilding() {
        String city = ioHandler.getStringFromUserForReason("Please enter the city");
        String street = ioHandler.getStringFromUserForReason("Please enter the street");
        int streetNumber = ioHandler.getIntFromUserForReason("Please enter the street number");
        int levels = ioHandler.getIntFromUserForReason("Please enter the amount of levels of your building");
        building = new Building(city, street, streetNumber, levels);
    }

    public void fillFloorWithRooms(int level) {
        if (level < 0) {
            ioHandler.println("Error you can not have negative floors");
            return;
        }
        Floor f = building.getFloor(level);
        if (f instanceof NullFloor) {
            ioHandler.println("Error the floor position exceeded the available amount of floors");
            return;
        }

        addRoomToFloor(f);
        ioHandler.println("Floor filled");
    }

    private void addRoomToFloor(Floor floor) {
        String roomName = ioHandler.getStringFromUserForReason("Please enter room name");
        floor.addRoom(roomName);
        ioHandler.println("Room " + roomName + " added!");
        String answer = ioHandler.getStringFromUserForReason("Do you wish to add an other room? Y=yes, ENTER to quit");
        if(!"Y".equals(answer)){
            return;
        }
        addRoomToFloor(floor);
    }

    public void fillRoom(int level, String roomName){
        if (level < 0) {
            ioHandler.println("Error you can not have negative floors");
            return;
        }
        Floor floorObj = building.getFloor(level);
        if (floorObj instanceof NullFloor) {
            ioHandler.println("Error the floor position exceeded the available amount of floors");
            return;
        }
        Room roomObj = floorObj.findRoom(roomName);
        if (roomObj instanceof NullRoom) {
            ioHandler.println("Error the room did not exist");
            ioHandler.println(floorObj.toString());
            return;
        }
        createDoorForRoom(roomObj);
        createWindowForRoom(roomObj);
        createLightForRoom(roomObj);
        ioHandler.println("Room filled");
    }

    private void createDoorForRoom(Room room){
        String doorName = ioHandler.getStringFromUserForReason("Please enter the door name");
        room.addDoor(doorName);
        String answer = ioHandler.getStringFromUserForReason("Do you wish to add an other door? Y=yes, ENTER to continue");
        if(!"Y".equals(answer)){
            return;
        }
        createDoorForRoom(room);
    }

    private void createWindowForRoom(Room room){
        String windowName = ioHandler.getStringFromUserForReason("Please enter the window name");
        room.addWindow(windowName);
        String answer = ioHandler.getStringFromUserForReason("Do you wish to add an other window? Y=yes, ENTER to continue");
        if(!"Y".equals(answer)){
            return;
        }
        createWindowForRoom(room);
    }

    private void createLightForRoom(Room room){
        String lightName = ioHandler.getStringFromUserForReason("Please enter the light name");
        room.addLight(lightName);
        String answer = ioHandler.getStringFromUserForReason("Do you wish to add an other light? Y=yes, ENTER to continue");
        if(!"Y".equals(answer)){
            return;
        }
        createLightForRoom(room);
    }


    @Override
    public String toString() {
        return "BuildingManager{" +
                "building=" + building +
                '}';
    }
}
