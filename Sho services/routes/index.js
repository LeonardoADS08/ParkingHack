var express = require('express');
var router = express.Router();
var ctrlUser = require("../controllers/user-controller.js");
var ctrlKeeper = require("../controllers/keeper-controller.js");
var ctrlParkingSpot = require("../controllers/parkingSpot-controller.js");
var ctrlHistory = require("../controllers/history-controller.js");
router.route("/prueba").get(ctrlUser.prueba);

//USER
router.route("/user")
.post(ctrlUser.addUser)
.get(ctrlUser.getUsers);

router.route("/user/:userId")
.get(ctrlUser.getUser)
.put(ctrlUser.updateUser)
.delete(ctrlUser.deleteUser);

//KEEPER
router.route("/keeper")
.post(ctrlKeeper.addKeeper)
.get(ctrlKeeper.getKeepers);

router.route("/keeper/:keeperId")
.get(ctrlKeeper.getKeeper)
.put(ctrlKeeper.updateKeeper)
.delete(ctrlKeeper.deleteKeeper);

//PARKING SPOTS
router.route("/parkingSpot")
.post(ctrlParkingSpot.addSpot)
.get(ctrlParkingSpot.getSpots);

router.route("/parkingSpot/:spotId")
.get(ctrlParkingSpot.getSpot)
.put(ctrlParkingSpot.updateSpot)
.delete(ctrlParkingSpot.deleteSpot);

router.route("/parkingSpotBoth").post(ctrlKeeper.addPark);

//HISTORY
router.route("/history")
.post(ctrlHistory.addHistory)
.get(ctrlHistory.getHistories);

router.route("/history/:historyId")
.put(ctrlHistory.updateHistory)
.get(ctrlHistory.getHistory)
.delete(ctrlHistory.deleteHistory);

module.exports = router;