const dataBar = {
    type: "bar",
    data: {
        labels: ["Monday", "Tuesday", "Wednesday", "Thursday"],
        datasets: [{
            label: "Traffic",
            data: [3237, 2234, 3234, 1234],
            backgroundColor: ["rgba(66,133,244,0.6)", "rgba(66,133,244,0.6)",
                "rgba(66,133,244,0.6)", "rgba(66,133,244,0.6)"
            ],
        },],
    },
};

new mdb.Chart(document.getElementById("chart-bar"), dataBar);