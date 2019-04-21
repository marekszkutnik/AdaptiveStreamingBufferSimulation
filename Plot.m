clear all;
close all;

fid = fopen('C:\Users\293275\Desktop\Studia\AISDE\Projekt2\TXT\PlotBuffer.txt'); % open file
[A com] = fscanf(fid, '%g ' ); % load to A
if(A<0)
  disp(com);  
end

fclose(fid);

time = fopen('C:\Users\293275\Desktop\Studia\AISDE\Projekt2\TXT\PlotTime.txt'); % open file
[t com] = fscanf(time, '%g ' ); % load to A
if(t<0)
  disp(com);  
end

fclose(time);

band = fopen('C:\Users\293275\Desktop\Studia\AISDE\Projekt2\TXT\PlotBand.txt'); % otwiera plik
[b com] = fscanf(band, '%g' ); % wczytuje do macierzy A
if(b<0)
  disp(com);  
end

fclose(band);

figure;
plot(t,A);
hold on;
grid on;
plot(t,b);
hold off;

