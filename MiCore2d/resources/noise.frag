#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform float texAlpha;
uniform float times;

float random(vec2 st, float times)
{
  return fract(sin(dot(st.xy, vec2(12.9898,78.233)))* 43758.5453123 * cos(times));
}

void main()
{
  vec2 st = texCoord.xy;
  float rnd = random(st, times);

  outputColor = mix(texture(texture0, texCoord), vec4(vec3(rnd), 1.0), texAlpha);
}
