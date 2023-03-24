#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform float texAlpha;
uniform float blur;
uniform float bloom;
uniform float width;
uniform float height;

void main()
{
  	float v;
	float pi = 3.141592653589793;
	float ew_step = 1.0 / width;
  	float eh_step = 1.0 / height;
	float radius = blurSize;
  	if ( radius < 0 ) radius = 0;

  	int steps = int(min(radius * 0.7, sqrt(radius) * pi));
	float r = radius / steps;
	float t = bloom / (steps * 2 + 1);
	float x = texCoord.x;
	float y = texCoord.y;
	
	vec4 sum = texture(texture0, vec2(x, y)) * t;
	int i;
	for(i = 1; i <= steps; i++){
		v = (cos(i / (steps + 1) / pi) + 1) * 0.5;
		sum += texture(texture0, vec2(x + i * ew_step * r, y)) * v * t;
		sum += texture(texture0, vec2(x - i * ew_step * r, y)) * v * t;
	}
  	for(i = 1; i <= steps; i++){
		v = (cos(i / (steps + 1) / pi) + 1) * 0.5;
		sum += texture(texture0, vec2(x, y + i * eh_step * r)) * v * t;
		sum += texture(texture0, vec2(x, y - i * eh_step * r)) * v * t;
	}
 
  	outputColor= mix(sum, vec4(0.0, 0.0, 0.0, 0.0), texAlpha);
}
