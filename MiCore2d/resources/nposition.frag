#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform sampler2D texture0;
uniform float texAlpha;

void main()
{
  vec4 color = texture(texture0, texCoord).rgba;
  color.r = 1.0 - color.r;
  color.g = 1.0 - color.g;
  color.b = 1.0 - color.b;
  outputColor = mix(color, vec4(0.0, 0.0, 0.0, 0.0), texAlpha);
}
