<template>
  <div class="hello">
    <img :src="'data:image/jpeg;base64,' + ImageData" />
  </div>
</template>

<script>

	var counter = 0;
	var startTime = new Date();
	setInterval(printStats, 10000);

	function printStats() {
		var currentTime = new Date();
		var elapsed = (currentTime - startTime)/1000;
		console.log(counter + ' calls in last ' + elapsed + 's');
		console.log(counter/elapsed + ' fps per second over ' + elapsed + 's time period');
		counter = 0;
		startTime = new Date();
	}


	export default {
	name: "HelloWorld",
	props: {
		ImageData: String
	},
	updated: function () {
		this.$nextTick(function () {
			counter++;
		})
	}
	};
</script>