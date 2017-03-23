
var app = angular.module('app');
app.controller("app.views.RequestForm",  function ($scope, $timeout, requestAJService,$window, appSession) {
    $scope.requests = [];
    $scope.emp = null;
    $scope.divAddRequest = false;
   
   
  
  function GetParticulars() {
        debugger;
        var getParticularData = requestAJService.getAllParticulars();

        getParticularData.success(function (particular) {
            debugger;
            $scope.particulars = particular;
        }, function () {
            debugger;
            alert('Error in getting particular records');
        });
    }
    $scope.SubmitRequestDiv=function()
    {
            

        var req = requestAJService.addRequest($scope.requests,appSession.tenant.id, appSession.user.id);
        getempData.then(function (msg) {
            $window.location.href = '#/test';
        }, function () {
            alert('Error in updating emp record');
        });
      

    }
    function GetUnits() {
        var getUnits = requestAJService.getUnits();

        getUnits.success(function (unit) {
            $scope.units = unit;
        }, function () {
            alert('Error in getting unit records');
        });
    }

    function BindData() {

        $scope.requests.push($scope.emp);

    }

    

    $scope.editRequest = function (request) {
       
        $scope.ParticularId = request.ParticularId;
        $scope.UnitId = request.UnitId;
        $scope.Rate = request.Rate;
        $scope.Vat = request.Vat;
        $scope.VateRate = request.VateRate;
        $scope.FinalRate = request.FinalRate;
        $scope.Quantity = request.Quantity;
        $scope.Cost = request.Cost;
        $scope.Action = "Update";
        $scope.updateIndex = request.Id;
        $scope.divAddRequest = true;
    }

    $scope.AddRequest = function () {
        var index = 0;
        if ($scope.requests.length != 0)
             index = $scope.requests.length;
        
    
        var varlues = $scope.employee;
        var emp = {
            Id: index,
            ParticularId: $scope.selected,
            UnitId: $scope.unitid,
            Rate: $scope.Rate,
            Vat: $scope.Vat,
            VateRate: $scope.VateRate,
            FinalRate: $scope.FinalRate,
            Quantity: $scope.Quantity,
            Cost: $scope.Cost,
            TenantId: appSession.tenant.id,
            UserId: appSession.user.id
    };

        $scope.divAddRequest = false;
        var getEmpAction = $scope.Action;
        if (getEmpAction == "Update") {
          
          
        }
        else {
            
            $scope.emp = emp;
            BindData();
        }
    }

    $scope.AddRequestDiv = function () {
        ClearFields();
        $scope.Action = "Add";
        $scope.divAddRequest = true;
        GetParticulars();
        GetUnits();
    }

    $scope.DeleteRequest = function (emp) {
        var getData = crudAJService.deleteEmployee(emp.EmployeeId);
        getData.then(function (msg) {
            alert(msg.data);
            GetAllEmployees();

        }, function () {
            alert('Error in deleting emp record');
        });
    }

    function ClearFields() {
        $scope.Name = ""
        $scope.EmployeeId = "";
        $scope.Designation = "";
        $scope.Dept = "";
    }
    $scope.Cancel = function () {
        $scope.divAddRequest = false;
        
    };
    $scope.RemoveDetails = function () {
      
      
        
    }
});

app.service("requestAJService", function ($http, $q) {
    
    this.getAllParticulars = function () {
        var response = $http({
            method: "get",
            url: "RequestForm/GetAllParticulars",
            dataType: "json"
        });
        return response;
    }

    this.getUnits = function () {
        var response = $http({
            method: "get",
            url: "RequestForm/GetUnits",
            dataType: "json"
        });
        return response;
    }

    this.getEmployee = function (EmployeeId) {
        var response = $http({
            method: "post",
            url: "Employee/GetEmployeeById",
            params: {
                id: JSON.stringify(EmployeeId)
            }
        });
        return response;
    }

    this.updateEmployee = function (emp) {
        var response = $http({
            method: "post",
            url: "Employee/UpdateEmployee",
            data: JSON.stringify(emp),
            dataType: "json"
        });
        return response;
    }

    this.addRequest = function (emp, tenant, user) {
        emp.tenant = tenant;
        var response = $http({
            method: "post",
            //url: "Employee/AddEmployee",
            url: "RequestForm/PostStationaryRequestDetails",
            data: JSON.stringify(emp),
            dataType: "json"
        });
        return response;
    }

    this.deleteEmployee = function (employeeId) {
        var response = $http({
            method: "post",
            url: "Employee/DeleteEmployee",
            params: {
                empId: JSON.stringify(employeeId)
            }
        });
        return response;
    }


});

app.config(['$httpProvider', function ($httpProvider) {
    //initialize get if not there
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    //disable IE ajax request caching
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
}]);


