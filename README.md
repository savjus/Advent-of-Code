Change Output Type to EXE:

	Open Visual Studio and load your Class Library project.
	Right-click the project in Solution Explorer → Properties.
	Go to the Application tab.
	Change Output type from Class Library to Console Application or Windows Application.
	Add a Main method (entry point) in one of the classes:

taken from https://github.com/rtrinh3/AdventOfCode/blob/master/README.md
	run day with .\Aoc2023.exe 01 "day01-input.txt"
	add inputs and execute the code in the filepath:
	\Aoc2023\Aoc2023\bin\Debug\net8.0




	scoop
	conda install -y numpy scipy pandas scikit-learn matplotlib seaborn transformers datasets tokenizers accelerate evaluate optimum huggingface_hub nltk category_encoders
	conda install -y pytorch torchvision torchaudio pytorch-cuda=12.4 -c pytorch -c nvidia
	pip install requests requests_toolbelt
	conda update --all			
	conda:
		python virtual enviroment.
		conda create -n ai python=3.11     - create enviroment
		conda activate (env)


		miniconda config:	
			conda config --add channels defaults
			conda config --add channels conda-forge
			conda config --add channels nvidia # only needed if you are on a PC that has a nvidia gpu
			conda config --add channels pytorch
			conda config --set channel_priority strict


	jupyter:
		launch - jupyter lab OR jupyter notebook    in terminal
		shift + enter to run,
		dd to delete cell

	Scikit-learn:
	pyTorch:
