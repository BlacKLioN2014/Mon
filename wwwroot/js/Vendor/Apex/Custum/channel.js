var options = {
	chart: {
		height: 250,
		type: "bar",
		toolbar: {
			show: false,
		},
	},
	plotOptions: {
		bar: {
			horizontal: true,
		},
	},
	dataLabels: {
		enabled: false,
	},
	grid: {
		borderColor: "#dfd6ff",
		strokeDashArray: 5,
		xaxis: {
			lines: {
				show: false,
			},
		},
		yaxis: {
			lines: {
				show: true,
			},
		},
		padding: {
			top: 0,
			right: 0,
			bottom: 0,
			left: 0,
		},
	},
	series: [
		{
			data: [2000, 3000, 4000, 5000, 6000],
		},
	],
	colors: ["#00368e", "#bad5f8", "#e8f1fd"],
	xaxis: {
		categories: ["Google", "TV Ads", "Social", "Friends", "Video"],
	},
	tooltip: {
		y: {
			formatter: function (val) {
				return val + " Visits";
			},
		},
	},
};

//var chart = new ApexCharts(document.querySelector("#channel"), options);

//chart.render();
