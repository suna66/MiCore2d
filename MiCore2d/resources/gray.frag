#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform float texAlpha;

void main()
{
  vec4 color = texture(texture0, texCoord).rgba;
  float g = color.r * 0.299 + color.g * 0.587 + color.b * 0.114;
  color.r = g;
  color.g = g;
  color.b = g;
  outputColor = mix(color, vec4(0.0, 0.0, 0.0, 0.0), texAlpha);
}
