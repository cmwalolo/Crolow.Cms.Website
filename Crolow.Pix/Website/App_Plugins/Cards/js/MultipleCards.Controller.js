angular.module("umbraco").controller("MultipleCardsController", function ($scope, mediaResource, contentResource) {
    vm = $scope;
    $scope.Data = $scope.block.data;
    $scope.Text = "couoou";

    cards = vm.Data.cards.split(",");

    const tmpCards = new Array();

    init();

    $scope.Cards = tmpCards;

    if (Array.isArray(tmpCards)) {
        console.log($scope.Cards);
        console.log("length :" + Object.values($scope.Cards).length);
    }
    $scope.columns = (12 / $scope.Cards.length);


    function init() {
        for (item = 0; item < cards.length; item++) {
            id = cards[item].replace("umb://document/", "");

            response = contentResource.getById(id).then(function (node) {
                card = ExtractValues(node, getCurrentCulture());
                tmpCards.push(card);
            });
        }
        function ExtractValues(data, culture) {
            value = {};
            for (var v = 0; v < data.variants.length; v++) {
                var variant = data.variants[v];
                if (variant.language.culture == culture) {
                    value["name"] = variant.name;
                    for (var t = 0; t < variant.tabs.length; t++) {
                        tab = variant.tabs[t];
                        for (var p = 0; p < tab.properties.length; p++) {
                            var prop = tab.properties[p];
                            if (typeof prop.value !== "function") {
                                value[prop.alias] = prop.value;
                            }
                        }
                    }
                }
            }
            return value;
        };

        function getCurrentCulture() {
            if (typeof $routeParams !== 'undefined') {
                return $routeParams.cculture ? $routeParams.cculture :
                    $routeParams.mculture ? $routeParams.mculture :
                        $cookies.get("UMB_MCULTURE") !== undefined ? $cookies.get("UMB_MCULTURE") : "en";
            }

            return "fr";
        }

    }
});


