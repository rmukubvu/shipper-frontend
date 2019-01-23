var restEndPointProd = "http://23.100.43.140";
var restEndPoint = "http://localhost:8089";

var user = localStorage.getItem('shipper.userName');

$(document).idle({
  onIdle: function(){
      location.href = "/Login/LockScreen";
  },
  idle: 600000
});

